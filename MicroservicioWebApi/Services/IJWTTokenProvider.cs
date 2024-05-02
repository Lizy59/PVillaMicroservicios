namespace MicroservicioWebApi.Services
{
	public interface IJWTTokenProvider
	{

		void SetToken(string token);
		string? GetToken();
		void ClearToken();


	}
}
