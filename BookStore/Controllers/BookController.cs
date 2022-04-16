#nullable disable
using BookStore.Model.DTOs;
using BookStore.Model.Entities;
using BookStore.Model.Exceptions;
using BookStore.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAutorRepository _autorRepository;

        public BookController(IBookRepository bookEepository, IAutorRepository autorRepository)
        {
            _bookRepository = bookEepository;
            _autorRepository = autorRepository;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _bookRepository.GetBooks();
                foreach(var book in books)
                {
                    var autor = await _autorRepository.GetAutorById(book.AutorId);
                    book.Autor = autor;
                }
                var bookResponse = JsonConvert.SerializeObject(books);
                var response = new OperationDTO("success", "", bookResponse);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return StatusCode(500, response);
            }
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookById(id);
                var autor = await _autorRepository.GetAutorById(book.AutorId);
                book.Autor = autor;
                var bookResponse = JsonConvert.SerializeObject(book);
                var response = new OperationDTO("success", "", bookResponse);
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

        // PUT: api/Book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook(int id, BookDTO bookDTO)
        {
            try
            {
                var autor = await _autorRepository.GetAutorById(bookDTO.AutorId);
                Book book = new(bookDTO.Title, bookDTO.Year, bookDTO.Category)
                {
                    Autor = autor,
                    AutorId = autor.AutorId
                };
                await _bookRepository.EditBook(id, book);
                var response = new OperationDTO("success", "La operación se realizó con éxito", "");
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

        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> InsertBook(BookDTO bookDTO)
        {
            try
            {
                var autor = await _autorRepository.GetAutorById(bookDTO.AutorId);
                Book book = new(bookDTO.Title, bookDTO.Year, bookDTO.Category)
                {
                    Autor = autor
                };
                await _bookRepository.InsertBook(book);
                var NewBook = CreatedAtAction("GetBook", new { id = book.BookId }, book);
                var bookResponse = JsonConvert.SerializeObject(NewBook.Value);
                var response = new OperationDTO("success", "La operación se realizó con éxito", bookResponse);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                var response = new OperationDTO("fail", ex.Message, "");
                return BadRequest(response);
            }
            catch (MaxLimitReachedException ex)
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

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookRepository.DeleteBook(id);
                var response = new OperationDTO("success", "El libro se eliminó con éxito", "");
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
    }
}
