using ApiLab.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99m },
            new Product { Id = 2, Name = "Phone", Price = 499.99m }
        };

        // GET: api/products
        [HttpGet]
        public IActionResult GetAll() => Ok(_products);

        // GET api/products/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return product == null ? NotFound() : Ok(product);
        }

        // POST api/products
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            _products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT api/products/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            var index = _products.FindIndex(p => p.Id == id);
            if (index == -1) return NotFound();
            _products[index] = product;
            return NoContent();
        }

        // DELETE api/products/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            _products.Remove(product);
            return NoContent();
        }
    }
}