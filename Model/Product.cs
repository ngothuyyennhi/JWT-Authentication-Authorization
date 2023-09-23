using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class Product
    {
        [Key] 
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
