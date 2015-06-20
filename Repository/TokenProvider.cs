using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;

namespace SPP.Repository
{
	public static class TokenProvider
	{
		private static Dictionary<int, string> _tokens;
		public static IDictionary<int, string> Tokens
		{
			get { return _tokens; }
		}
		
		public static void InitializeTokenProvider()
		{
			_tokens = new Dictionary<int, string>();
		}
		
		public static string GenerateNewToken(int userId)
		{
			if (!_tokens.ContainsKey(userId))
			{
				_tokens.Add(userId, Guid.NewGuid().ToString());
				return _tokens[userId];
			}
			return null;
		}
		
		public static bool Authorized(HttpContext context)
		{
			string token = context.Request.Headers["Authorization"];
			return _tokens.ContainsValue(token);
		}
	}
}