namespace BookStore.Model.Entities
{
    public class Autor
    {
        public int AutorId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? City { get; set; }
        public string Email { get; set; }
        public Autor (string name, string lastname, DateTime? birthdate, string? city, string email)
        {
            Name = name;
            Lastname = lastname;
            Birthdate = birthdate;
            City = city;
            Email = email;
        }
    }
}
