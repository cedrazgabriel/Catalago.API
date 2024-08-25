using Catalago.API.Context;
using Catalago.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalago.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var categories = context.Categories.AsNoTracking().ToList();

            if (categories is null)
            {
                return NotFound();
            }

            return categories;
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            return context.Categories.AsNoTracking().Include(p => p.Products).ToList();
        }

        [HttpGet("{id:int}", Name = "GetCategories")]
        public ActionResult<Category> GetCategories(int id)
        {
            var category = context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);

            if (category is null)
            {
                return NotFound("Category not found");
            }

            return category;
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category is null)
            {
                return BadRequest();

            }
            context.Categories.Add(category);
            context.SaveChanges();

            return new CreatedAtRouteResult("GetCategories",
                new { id = category.Id },
                category);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (category is null)
            {
                return BadRequest();
            }

            context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok("Category updated");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var category = context.Products.FirstOrDefault(p => p.Id == id);

            if (category is null)
            {
                return NotFound("Category not found");
            }

            context.Products.Remove(category);
            context.SaveChanges();

            return Ok();
        }
    }
}
