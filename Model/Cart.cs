using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class Cart
    {
        [Key]
        public string? Cart_ID { get; set; }

        [Required]
        public long Total { get; set; }

        public Cart()
        {
            
        }
        public Cart(string Cart_ID)
        {
            Total = 0;
            this.Cart_ID = Cart_ID;
        }

        public virtual ICollection<CartDetail> cart_detail { get; set; }
        public Cart(string Cart_ID, long Total)
        {
            Total = 0;
            this.Cart_ID = Cart_ID;
        }
    }
}
