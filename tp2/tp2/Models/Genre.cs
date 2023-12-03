namespace tp2.Models
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // a genre is related to many movies
        public ICollection<Movie> Movies { get; set; }
    }
}
