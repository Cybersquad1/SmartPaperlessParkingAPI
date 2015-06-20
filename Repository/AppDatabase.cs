using System;
using System.Collections.Generic;
using System.Linq;
using SPP.Models;

namespace SPP.Repository
{
	public static class AppDatabase
	{
		private static List<Establishment> _establishments;
		public static ICollection<Establishment> Establishments
		{
			get { return _establishments;}
		}
		
		private static List<User> _users;
		public static ICollection<User> Users
		{
			get { return _users;}
		}
		
		private static List<Entry> _entries;
		public static List<Entry> Entries
		{
			get { return _entries;}
		}
		
		
		public static void SeedDatabase()
		{
			List<Establishment> establishments = new List<Establishment>
			{
				new Establishment() { Id = 1, Name = "Establishment 1", TotalSpots = 150, InitialPrice = 4, PricePerHour = 1 },
				new Establishment() { Id = 2, Name = "Establishment 2", TotalSpots = 300, InitialPrice = 3, PricePerHour = 2 },
				new Establishment() { Id = 3, Name = "Establishment 3", TotalSpots = 450, InitialPrice = 2, PricePerHour = 1 }
			};
			_establishments = establishments;
			
			List<User> users = new List<User>
			{
				new User() { Id = 1, Name = "C. F. Frost", Email = "foo@bar.com", Password = "abc123" },
				new User() { Id = 2, Name = "John Muller", Email = "fine@email.com", Password = "abc123" },
				new User() { Id = 3, Name = "Aaron Bennet", Email = "abc@123.com", Password = "abc123" }
			};
			_users = users;
			
			List<Entry> entries = new List<Entry>
			{
				new Entry() { Id = 1, Establishment = _establishments.FirstOrDefault(x => x.Id == 1),
					User = _users.FirstOrDefault(x => x.Id == 1), EntryTime = DateTime.Now,
					IsClosed = false }
			};
			_entries = entries;
		}
	}
}