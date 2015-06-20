using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;
using SPP.Repository;

namespace SPP.Controllers
{
	[Route("api/[controller]")]
	public class TokensController : Controller
	{
		public TokensController()
		{
			if (TokenProvider.Tokens == null)
				TokenProvider.InitializeTokenProvider();
			if (AppDatabase.Users == null)
				AppDatabase.SeedDatabase();
		}
		
		[HttpGet]
		public IDictionary<int, string> GetAll()
		{
			return TokenProvider.Tokens;
		}
		
		[HttpPost]
		public IActionResult Token(string email, string password)
		{
			var user = AppDatabase.Users.FirstOrDefault(x => x.Email.Equals(email));
			var token = "";
			
			if (user == null)
			{
				return HttpNotFound();
			}
			if (user.Password.Equals(password))
			{
				token = TokenProvider.GenerateNewToken(user.Id);
				if (token == null)
					token = TokenProvider.Tokens[user.Id];
			}
			return new ObjectResult(token);
		}
	}
}