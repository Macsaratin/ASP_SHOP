

using Microsoft.AspNetCore.Mvc;
using ASP_SHOP.Data;
using ASP_SHOP.Models;
using System.Linq;

//Dùng swager

namespace ASP_SHOP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (string.IsNullOrEmpty(product.ImageUrl))
            {
                product.ImageUrl = "/images/default.png"; 
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null) return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                existingProduct.ImageUrl = product.ImageUrl;
            }

            _context.SaveChanges();
            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok(new { message = "Deleted successfully" });
        }
    }
}