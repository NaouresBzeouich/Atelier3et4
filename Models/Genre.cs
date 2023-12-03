using System.ComponentModel.DataAnnotations;

namespace atelier3.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }

    }
}
