using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ApplicationDBContext _dbContext;

		public CategoryController(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
		{
			var categories = await _dbContext.Categories
				.Include(c => c.Products)
				.ToListAsync();
			return Ok(categories);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Category>> GetCategory(int id)
		{
			var category = await _dbContext.Categories
				.Include(c => c.Products)
				.FirstOrDefaultAsync(c => c.CategoryId == id);

			if (category == null)
			{
				return NotFound(new { message = "Category Not Found" });
			}

			return Ok(category);
		}

		[HttpPost]
		public async Task<ActionResult<Category>> CreateCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				_dbContext.Categories.Add(category);
				await _dbContext.SaveChangesAsync();
				return Ok(category);
			}
			return BadRequest(ModelState);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(int id, Category category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingCategory = await _dbContext.Categories.FindAsync(id);

			if (existingCategory == null)
			{
				return NotFound(new { message = "Category Not Found" });
			}

			existingCategory.Name = category.Name;
			existingCategory.Description = category.Description;

			await _dbContext.SaveChangesAsync();

			return Ok(new { message = "Category updated successfully" });
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingCategory = await _dbContext.Categories.FindAsync(id);

			if (existingCategory == null)
			{
				return NotFound(new { message = "Category Not Found" });
			}

			_dbContext.Categories.Remove(existingCategory);
			await _dbContext.SaveChangesAsync();
			return Ok(new { message = "Category removed successfully" });
		}
	}
}