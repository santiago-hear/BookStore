using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Model.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Category { get; set; }
        [ForeignKey("AutorId")]
        public virtual Autor Autor { get; set; }
        public virtual int AutorId { get; set; }
        public Book(string title, string year, string category)
        {
            Title = title;
            Year = year;
            Category = category;
        }
    }
}
