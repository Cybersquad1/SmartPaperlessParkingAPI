using System;

namespace SPP.Models
{
	public class Entry
	{
		public int Id { get; set; }
		
		public Establishment Establishment { get; set; }
		
		public User User { get; set; }
		
		public DateTime EntryTime { get; set; }
		
		private decimal _duePrice;
		public decimal DuePrice
		{
			get
			{ 
				if (!IsClosed)
				{
					var totalHours = (int) DateTime.Now.Subtract(EntryTime).TotalHours;
					_duePrice = Establishment.InitialPrice + (totalHours * Establishment.PricePerHour);
				}
				return _duePrice;
			}
			private set { _duePrice = value; }
		}
		
		public bool IsClosed { get; set; }
		
		public DateTime ExitTime { get; set; }
	}
}