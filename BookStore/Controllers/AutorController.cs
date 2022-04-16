#nullable disable
using BookStore.Model.DTOs;
using BookStore.Model.Entities;
using BookStore.Model.Exceptions;
using BookStore.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorRepository _autorRepository;

        public AutorController(IAutorRepository repository)
        {
            _autorRepository = repository;
        }

        // GET: api/Autor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutors()
        {
            try
            {
                var autors = await _autorRepository.GetAutors();
                var autorResponse = JsonConvert.SerializeObject(autors);
                var response = new OperationDTO("success", "", autorResponse);
                return Ok(response); 
            }
            catch (Exception ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return StatusCode(500, response);
            }
        }

        // GET: api/Autor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            try
            {
                var autor = await _autorRepository.GetAutorById(id);
                var autorResponse = JsonConvert.SerializeObject(autor);
                var response = new OperationDTO("success", "", autorResponse);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return StatusCode(500, response);
            }
        }

        // PUT: api/Autor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAutor(int id, AutorDTO autorDTO)
        {
            try
            {
                var date = DateTime.Parse(autorDTO.Birthdate);
                var autor = new Autor(autorDTO.Name, autorDTO.Lastname, date, autorDTO.City, autorDTO.Email);
                await _autorRepository.EditAutor(id, autor);
                var response = new OperationDTO("success", "La operación se realizó con éxito", "");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return NotFound(response);
            }
            catch (FormatException ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return StatusCode(500, response);
            }
        }

        // POST: api/Autor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Autor>> InsertAutor(AutorDTO autorDTO)
        {
            try
            {
                var date = DateTime.Parse(autorDTO.Birthdate);
                var autor = new Autor(autorDTO.Name, autorDTO.Lastname, date, autorDTO.City, autorDTO.Email);
                await _autorRepository.InsertAutor(autor);
                var NewAutor = CreatedAtAction("GetAutor", new { id = autor.AutorId }, autor);
                var autorResponse = JsonConvert.SerializeObject(NewAutor.Value);
                var response = new OperationDTO("success", "La operación se realizó con éxito", autorResponse);
                return Ok(response);
            }
            catch (FormatException ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return StatusCode(500, response);
            }
        }

        // DELETE: api/Autor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            try
            {
                await _autorRepository.DeleteAutor(id);
                var response = new OperationDTO("success", "La operación se realizó con éxito", "");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return NotFound(response);
            }
            catch (DbUpdateException ex)
            {
                var response = new OperationDTO("fail", "No se pudo eliminar el autor, puede que esté asociado a un libro", "");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = new OperationDTO("fail", ex.ToString(), "");
                return StatusCode(500, response);
            }
        }
    }
}
