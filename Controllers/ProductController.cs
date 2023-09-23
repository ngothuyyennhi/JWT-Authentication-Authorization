using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration config;
        private readonly IProductService service;

        public ProductController(ApplicationDbContext context, IConfiguration config, IProductService service)
        {
            this.context = context;
            this.config = config;
            this.service = service;
        }
        // GET: HomeController
        [Authorize(Roles = "Admin")]
        [HttpGet("test")]
        public ActionResult<string> Index()
        {
            return "Welcome Admin";
        }

        [HttpGet("all")]
        public ActionResult GetProducts()
        {
            return Ok(service.GetProducts());
        }

        [HttpGet("{id}")]
        public ActionResult GetProductById([FromRoute] int id)
        {
            Product findProduct = service.GetProductById(id);
            if (findProduct != null) return Ok(findProduct);
            else return BadRequest("Not Found Product");
        }

        [HttpPost("create")]
        public ActionResult CreateProducts([FromBody] Product product)
        {
            Product res = service.CreateProduct(product);
            return res == null ? Ok(res) : BadRequest("Product is already exist");

        }

        [HttpPost("delete/{id}")]
        public ActionResult DeleteProduct([FromRoute] int id)
        {
            if (service.DeleteProduct(id))
            {
                return Ok();
            }
            else return BadRequest("NotFound Product");
        }

        [HttpPut("update")]
        public ActionResult UpdateProduct([FromBody]Product product) 
        {
            if (service.UpdateProduct(product)) return Ok(product); else return BadRequest();
        }


        

    }
}
