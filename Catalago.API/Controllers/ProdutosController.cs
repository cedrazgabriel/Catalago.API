using Catalago.API.Context;
using Catalago.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalago.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
      private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        { 
            var products = _context.Products.ToList();

            if(products is null)
            {
                return NotFound();
            }

            return products;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product not found");
            }

            return product;
        }
    }
}
