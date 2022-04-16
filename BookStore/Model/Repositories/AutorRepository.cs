using BookStore.Model.Entities;
using BookStore.Model.Exceptions;
using BookStore.Model.Interfaces;
using BookStore.Model.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Model.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BookStoreSqlServerContext _context;
        public AutorRepository(BookStoreSqlServerContext context)
        {
            _context = context;
        }
        public async Task DeleteAutor(int id)
        {
            try
            {
                var autor = await _context.Autors.FindAsync(id);
                if(autor == null)
                {
                    throw new NotFoundException("El autor no está registrado");
                }
                _context.Autors.Remove(autor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task EditAutor(int id, Autor autor)
        {
            try
            {
                if (!AutorExists(id))
                {
                    throw new NotFoundException("El autor no está registrado");
                }
                var Sautor = await _context.Autors.FindAsync(id);
                Sautor.Name = autor.Name;
                Sautor.Lastname = autor.Lastname;
                Sautor.Birthdate = autor.Birthdate;
                Sautor.City = autor.City;
                Sautor.Email = autor.Email;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Autor> GetAutorById(int id)
        {
            try
            {
                var autor = await _context.Autors.FindAsync(id);
                if(autor == null)
                {
                    throw new NotFoundException("El autor no está registrado");
                }
                return autor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Autor>> GetAutors()
        {
            try
            {
                return await _context.Autors.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Autor> InsertAutor(Autor autor)
        {
            try
            {
                _context.Autors.Add(autor);
                await _context.SaveChangesAsync();
                return autor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool AutorExists(int id)
        {
            return _context.Autors.Any(e => e.AutorId == id);
        }
    }
}
