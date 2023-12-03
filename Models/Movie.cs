using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atelier3.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public int?  GenreId { get; set; } // la relation entre genre et movie est 0 ou 1 à plusieurs 

        public ICollection<Genre> Genres { get; set; }

        public DateTime? MovieAdded { get; set; }
        public string? Photo { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }



    }
}
