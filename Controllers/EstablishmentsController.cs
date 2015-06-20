using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;
using SPP.Models;
using SPP.Repository;

namespace SPP.Controllers	
{
	[Route("api/[controller]")]
	public class EstablishmentsController : Controller
	{
		public EstablishmentsController()
		{
			if (AppDatabase.Establishments == null)
				AppDatabase.SeedDatabase();
		}
		
		[HttpGet]
		public IEnumerable<Establishment> GetAll()
		{
			return AppDatabase.Establishments;
		}
		
		[HttpGet("{id:int}", Name = "GetEstablishmentByIdRoute")]
		public IActionResult GetById(int id)
		{
			if (TokenProvider.Authorized(Context))
			{
				var item = AppDatabase.Establishments.FirstOrDefault(x => x.Id == id);
				if (item == null)
				{
					return HttpNotFound();
				}
				
				return new ObjectResult(item);
			}
			return new HttpStatusCodeResult(401);
		}
		
		[HttpPost]
		public void Create([FromBody] Establishment item)
		{
			if (!ModelState.IsValid)
			{
				Context.Response.StatusCode = 400;
			}
			else
			{
				item.Id = AppDatabase.Establishments.Last().Id + 1;
				AppDatabase.Establishments.Add(item);
				
				string url = Url.RouteUrl("GetEstablishmentByIdRoute", new { id = item.Id },
					Request.Scheme, Request.Host.ToUriComponent());
				Context.Response.StatusCode = 201;
				Context.Response.Headers["Location"] = url;
			}
		}
		
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id)
		{
			var item = AppDatabase.Establishments.FirstOrDefault(x => x.Id == id);
			if (item == null)
			{
				return HttpNotFound();
			}
			AppDatabase.Establishments.Remove(item);
			return new HttpStatusCodeResult(204);
		} 
	}
}