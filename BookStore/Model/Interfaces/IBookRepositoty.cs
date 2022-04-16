using BookStore.Model.Entities;

namespace BookStore.Model.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task EditBook(int id, Book book);
        Task<Book> InsertBook(Book book);
        Task DeleteBook(int id);
    }
}
