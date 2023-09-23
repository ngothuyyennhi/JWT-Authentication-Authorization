using WebApplication2.Model;

namespace WebApplication2.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {

            this.context = context;
        }
        public IEnumerable<Product> GetProducts()
        {
            return context.Products;
        }

        public Product GetProductById(int id)
        {
            return context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public Product GetProductByName(string name)
        {
            return context.Products.FirstOrDefault(p => p.ProductName == name);
        }

        public Product CreateProduct(Product product)
        {
            Product exist = GetProductByName(product.ProductName);
            if (exist != null)
            {
                return null;
            }
            else
            {
                context.Add(product);
                context.SaveChanges();
                return product;
            }
        }

        public bool UpdateProduct(Product product)
        {
            Product exist = GetProductByName(product.ProductName);
            if (exist != null)
            {
                
                exist.ProductName = product.ProductName;
                exist.Quantity = product.Quantity;
                context.Update(exist);
                context.SaveChanges();

                return true;
            }
            else return false;
        }
        public bool DeleteProduct(int id)
        {
            Product exist = context.Products.Find(id);
            if (exist != null)
            {
                context.Remove(exist);
                context.SaveChanges();

                return true;
            }
            else return false;
            
        }
    }
}

