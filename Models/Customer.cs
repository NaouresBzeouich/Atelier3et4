using System.ComponentModel.DataAnnotations;

namespace atelier3.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MembershipTypeId { get; set; } // car la relation entre customer et membershiptye 0 ou 1 à plusieurs 

        public MembershipType? membershipType { get; set; }

        public ICollection<Movie>? Movies { get; set; }

    }
}
