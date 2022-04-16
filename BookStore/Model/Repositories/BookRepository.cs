using BookStore.Model.Entities;
using BookStore.Model.Exceptions;
using BookStore.Model.Interfaces;
using BookStore.Model.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Model.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreSqlServerContext _context;
        private readonly IConfiguration _configuration;
        private readonly int maxBooks;
        public BookRepository(BookStoreSqlServerContext context)
        {
            _context = context;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            maxBooks = int.Parse(_configuration["MaxBooksAllowed"]);
        }
        public async Task DeleteBook(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if(book == null)
                {
                    throw new NotFoundException("El libro no se encuentra registrado");
                }
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task EditBook(int id, Book book)
        {
            try
            {
                if (!BookExists(id))
                {
                    throw new Exception("El libro no se encuentra registrado");
                }
                var Sbook = await _context.Books.FindAsync(id);
                Sbook.Title = book.Title;
                Sbook.Category = book.Category;
                Sbook.Year = book.Year;
                Sbook.AutorId = book.AutorId;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Book> GetBookById(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    throw new NotFoundException("El libro no se encuentra registrado");
                }
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            try
            {
                return await _context.Books.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Book> InsertBook(Book book)
        {
            try
            {
                if (_context.Books.Count() + 1 > maxBooks)
                {
                    throw new MaxLimitReachedException("No es posible registrar el libro, se alcanzó el máximo permitido");
                }
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
