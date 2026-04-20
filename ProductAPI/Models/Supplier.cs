using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProductAPI.Models;

namespace ProductManagementAPI.Models
{
	public class Supplier
	{
		public int SupplierId { get; set; }

		[Required]
		[MaxLength(150)]
		public string Name { get; set; } = string.Empty;

		[MaxLength(200)]
		public string? ContactEmail { get; set; }

		[MaxLength(20)]
		public string? Phone { get; set; }

		[MaxLength(300)]
		public string? Address { get; set; }

		// Navigation property
		public ICollection<Product> Products { get; set; } = new List<Product>();
	}
}