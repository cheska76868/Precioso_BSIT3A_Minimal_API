using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProductAPI.Models;

namespace ProductManagementAPI.Models
{
	public class Category
	{
		public int CategoryId { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; } = string.Empty;

		[MaxLength(255)]
		public string? Description { get; set; }

		// Navigation property
		public ICollection<Product> Products { get; set; } = new List<Product>();
	}
}