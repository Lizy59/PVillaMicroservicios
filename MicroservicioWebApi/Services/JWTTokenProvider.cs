namespace MicroservicioWebApi.Services
{
	public class JWTTokenProvider:IJWTTokenProvider
	{
		private readonly IHttpContextAccessor _contextAccessor;
		public JWTTokenProvider(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}
		public void ClearToken()
		{
			throw new NotImplementedException();
		}

		public string? GetToken()
		{
			throw new NotImplementedException();
		}

		public void SetToken(string token)
		{
			_contextAccessor.HttpContext.Response.Cookies.Append("TokenCookie", token);
		}
	}
}
