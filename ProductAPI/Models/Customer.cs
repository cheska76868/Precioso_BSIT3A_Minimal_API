using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.Models
{
	public class Customer
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		[MaxLength(100)]
		public string LastName { get; set; } = string.Empty;

		[MaxLength(200)]
		public string? Email { get; set; }

		[MaxLength(20)]
		public string? Phone { get; set; }

		[MaxLength(300)]
		public string? Address { get; set; }
	}
}