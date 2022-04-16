using BookStore.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Model.Repositories.Context
{
    public class BookStoreSqlServerContext : DbContext
    {
        public BookStoreSqlServerContext() { }
        public BookStoreSqlServerContext(DbContextOptions<BookStoreSqlServerContext> options) : base(options) { }
        public virtual DbSet<Book>? Books { get; set; }
        public virtual DbSet<Autor>? Autors { get; set; }
    }
}
