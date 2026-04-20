using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductManagementAPI.Models;

namespace ProductAPI.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		[Range(1, 1000000)]
		public decimal Price { get; set; }
		[Range(0, 10000)]
		public int Stock { get; set; }

			// Foreign keys
			public int CategoryId { get; set; }
			public int SupplierId { get; set; }

			// Navigation properties
			public Category? Category { get; set; }
			public Supplier? Supplier { get; set; }
	}
}