using BookStore.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess;

namespace BookStore.Model.Repositories.Context
{
    public class BookStoreOracleContext : DbContext
    {
        public BookStoreOracleContext() { }
        public BookStoreOracleContext(DbContextOptions<BookStoreOracleContext> options) : base(options) { }
        public virtual DbSet<Book>? Books { get; set; }
        public virtual DbSet<Autor>? Autors { get; set; }
    }
}
