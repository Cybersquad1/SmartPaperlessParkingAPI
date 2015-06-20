using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using SPP.Models;
using SPP.Repository;

namespace SPP.Controllers
{
	[Route("api/[controller]")]
	public class EntriesController : Controller
	{
		public EntriesController()
		{
			if (AppDatabase.Entries == null)
				AppDatabase.SeedDatabase();
		}
		
		[HttpGet]
		public IActionResult GetAll()
		{
			return new ObjectResult(AppDatabase.Entries);
		}
		
		[HttpGet("{id:int}", Name = "GetEntryByIdRoute")]
		public IActionResult GetEntryById(int id)
		{
			var entry = AppDatabase.Entries.FirstOrDefault(x => x.Id == id);
			if (entry == null)
			{
				return HttpNotFound();
			}
			return new ObjectResult(entry);
		}
		
		[HttpPost("{establishmentId:int}/{userId:int}", Name = "NewEntryRoute")]
		public void NewEntry(int establishmentId, int userId)
		{
			var user = AppDatabase.Users.FirstOrDefault(x => x.Id == userId);
			var establishment = AppDatabase.Establishments.FirstOrDefault(x => x.Id == establishmentId);
			if (user == null || establishment == null)
			{
				Context.Response.StatusCode = 400;
			}
			
			var entry = new Entry
			{
				Id = AppDatabase.Entries.Last().Id + 1,
				Establishment = establishment,
				User = user,
				EntryTime = DateTime.Now,
				IsClosed = false
			};
			AppDatabase.Entries.Add(entry);
			
			string url = Url.RouteUrl("GetEntryByIdRoute", new { id = entry.Id },
				Request.Scheme, Request.Host.ToUriComponent());
			Context.Response.StatusCode = 201;
			Context.Response.Headers["Location"] = url;
		}
		
		[HttpPut("pay/{userId:int}")]
		public IActionResult PayEntry(int userId)
		{
			var entry = AppDatabase.Entries.FirstOrDefault(x => x.User.Id == userId
				&& x.IsClosed == false);
			if (entry != null)
			{
				var index = AppDatabase.Entries.FindIndex(x => x.Equals(entry));
				
				entry.IsClosed = true;
				entry.ExitTime = DateTime.Now;
				AppDatabase.Entries[index] = entry;
				
				return new ObjectResult(entry);
			}
			return HttpNotFound();
		}
	}
}