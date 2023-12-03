using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tp2.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        [ForeignKey("Genre")]
        public Guid GenreId { get; set; }

        // a movie is related to a genre
        public virtual Genre Genre { get; set; }
        public Movie()
        {
        }
    }
}
