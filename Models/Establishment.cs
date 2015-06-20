using System.ComponentModel.DataAnnotations;

namespace SPP.Models
{
	public class Establishment
	{
		public int Id { get; set; }
		
		[Required]
		public string Name { get; set; }
		
		public int TotalSpots { get; set; }
		
		public decimal InitialPrice { get; set; }
		
		public decimal PricePerHour { get; set; }
	}
}