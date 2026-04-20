using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SupplierController : ControllerBase
	{
		private readonly ApplicationDBContext _dbContext;

		public SupplierController(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
		{
			var suppliers = await _dbContext.Suppliers.ToListAsync();
			return Ok(suppliers);
		}

		[HttpPost]
		public async Task<ActionResult<Supplier>> CreateSupplier(Supplier supplier)
		{
			if (ModelState.IsValid)
			{
				_dbContext.Suppliers.Add(supplier);
				await _dbContext.SaveChangesAsync();
				return Ok(supplier);
			}
			return BadRequest(ModelState);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutSupplier(int id, Supplier supplier)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingSupplier = await _dbContext.Suppliers.FindAsync(id);

			if (existingSupplier == null)
			{
				return NotFound(new { message = "Supplier Not Found" });
			}

			existingSupplier.Name = supplier.Name;
			existingSupplier.ContactEmail = supplier.ContactEmail;
			existingSupplier.Phone = supplier.Phone;
			existingSupplier.Address = supplier.Address;

			await _dbContext.SaveChangesAsync();

			return Ok(new { message = "Supplier updated successfully" });
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSupplier(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existingSupplier = await _dbContext.Suppliers.FindAsync(id);

			if (existingSupplier == null)
			{
				return NotFound(new { message = "Supplier Not Found" });
			}

			_dbContext.Suppliers.Remove(existingSupplier);
			await _dbContext.SaveChangesAsync();
			return Ok(new { message = "Supplier removed successfully" });
		}
	}
}
