using Microsoft.AspNetCore.Identity;

namespace MicroservicioAuthenticationApi.Services.Interfaces
{
	public interface IJWTTokenGenerator
	{
		string GenerateToken(IdentityUser identity);
	}
}
