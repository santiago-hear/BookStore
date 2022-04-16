using BookStore.Model.Entities;

namespace BookStore.Model.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> GetAutors();
        Task<Autor> GetAutorById(int id);
        Task EditAutor(int id, Autor autor);
        Task<Autor> InsertAutor(Autor autor);
        Task DeleteAutor(int id);
        bool AutorExists(int id);
    }
}
