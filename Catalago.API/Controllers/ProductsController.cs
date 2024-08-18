using Catalago.API.Context;
using Catalago.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalago.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
      private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
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

        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product not found");
            }

            return product;
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null) {
                return BadRequest();
            
            }
            _context.Products.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProduct",
                new {id = product.Id},
                product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (product is null)
            {
                return BadRequest();
            }

           _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           _context.SaveChanges();
            
            return Ok("Product updated");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product not found");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok();
        }
    }
}
