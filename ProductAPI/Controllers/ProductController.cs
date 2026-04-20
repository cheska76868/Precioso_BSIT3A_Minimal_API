using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ApplicationDBContext _dbContext;

		public ProductController(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var products = await _dbContext.Products.ToListAsync();
			return Ok(products);
		}
		
		[HttpPost]
		public async Task<ActionResult<Product>> CreateProduct(Product product)
		{
			if (ModelState.IsValid)
			{
				_dbContext.Products.Add(product);
				await _dbContext.SaveChangesAsync();
				return Ok(product);
			}
			return BadRequest(ModelState);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id, Product product)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingProduct = await _dbContext.Products.FindAsync(id);

			if (existingProduct == null)
			{
				return NotFound(new { message = "Product Not Found" });
			}

			existingProduct.Name = product.Name;
			existingProduct.Description = product.Description;
			existingProduct.Price = product.Price;
			existingProduct.Stock = product.Stock;
			existingProduct.CategoryId = product.CategoryId;
			existingProduct.SupplierId = product.SupplierId;

			await _dbContext.SaveChangesAsync();

			return Ok(new { message = "Product updated successfully" });
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingProduct = await _dbContext.Products.FindAsync(id);

			if (existingProduct == null)
			{
				return NotFound(new { message = "Product Not Found" });
			}

			_dbContext.Products.Remove(existingProduct);
			await _dbContext.SaveChangesAsync();
			return Ok(new { message = "Product removed successfully" });
		}
	}
}
