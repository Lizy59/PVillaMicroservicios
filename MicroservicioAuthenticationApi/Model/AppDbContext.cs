using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioAuthenticationApi.Model
{
	public class AppDbContext: IdentityDbContext
	{
		public AppDbContext()
		{

		}

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}


	}

}
