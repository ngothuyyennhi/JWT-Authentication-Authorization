using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Model
{
    public class CartDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CartDetail_Id { get; set; }
        public string? Cart_Id { get; set; }
        public int Product_Id { get; set; }

        public int Quantity { get; set; }
        public int Price { get; set; }

        public virtual Cart cart { get; set; }
        public CartDetail()
        {
            
        }
        public CartDetail(string Cart_Id, int Product_Id, int Quantity, int Price )
        {
            this.Cart_Id = Cart_Id;
            this.Product_Id = Product_Id;
            this.Quantity = Quantity;
            this.Price = Price;
        }


    }
}
