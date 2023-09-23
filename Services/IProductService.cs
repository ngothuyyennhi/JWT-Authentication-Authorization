using WebApplication2.Model;

namespace WebApplication2.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProductById(int id);
        public Product GetProductByName(string name);

        public Product CreateProduct(Product product);

        public bool UpdateProduct(Product product);
        public bool DeleteProduct(int id);

    }
}