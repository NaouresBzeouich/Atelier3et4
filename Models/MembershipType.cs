using System.ComponentModel.DataAnnotations;

namespace atelier3.Models
{
    public class MembershipType
    {
        public  int Id { get; set; }
        public float SignUpFee { get; set; }
        public int DurationInMonth { get; set; }    
        public int DiscountRate { get; set; }   
        public string Name { get; set; }

       public ICollection<Customer> customers {  get; set; } 
    }
}
