using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;
using SPP.Models;
using SPP.Repository;

namespace SPP.Controllers
{
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		public UsersController()
		{
			if (AppDatabase.Users == null)
				AppDatabase.SeedDatabase();
		}
		
		[HttpGet]
		public IEnumerable<User> GetAll()
		{
			return AppDatabase.Users;
		}
		
		[HttpGet("{id:int}", Name = "GetUserByIdRoute")]
		public IActionResult GetUserById(int id)
		{
			var user = AppDatabase.Users.FirstOrDefault(x => x.Id == id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return new ObjectResult(user);
		}
		
		[HttpGet("{id:int}/entries", Name = "GetEntriesByUserId")]
		public IActionResult GetEntriesByUserId(int id)
		{
			var user = AppDatabase.Users.FirstOrDefault(x => x.Id == id);
			var entries = AppDatabase.Entries.Where(x => x.User.Equals(user) && !x.IsClosed);
			if (user == null)
			{
				return HttpNotFound();
			}
			
			if (entries.Count() == 0)
			{
				return new HttpStatusCodeResult(204);
			}
			return new ObjectResult(entries);
		}
		
		[HttpPost]
		public void Create(User user)
		{
			if (!ModelState.IsValid)
			{
				Context.Response.StatusCode = 400;
			}
			else
			{
				user.Id = AppDatabase.Users.Last().Id + 1;
				AppDatabase.Users.Add(user);
				
				string url = Url.RouteUrl("GetUserByIdRoute", new { id = user.Id },
					Request.Scheme, Request.Host.ToUriComponent());
				Context.Response.StatusCode = 201;
				Context.Response.Headers["Location"] = url;
			}
		}
		
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id)
		{
			var user = AppDatabase.Users.FirstOrDefault(x => x.Id == id);
			if (user == null)
			{
				return HttpNotFound();
			}
			AppDatabase.Users.Remove(user);
			return new HttpStatusCodeResult(204);
		}
	}
}