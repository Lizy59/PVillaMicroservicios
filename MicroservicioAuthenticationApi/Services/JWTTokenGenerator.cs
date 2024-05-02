using MicroservicioAuthenticationApi.Model;
using MicroservicioAuthenticationApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MicroservicioAuthenticationApi.Services
{
	public class JWTTokenGenerator : IJWTTokenGenerator
	{
		// Campo privado para almacenar las opciones de configuración de JWT.
		private readonly JwtOptions _jwtoptions;

		// Constructor de la clase que inicializa el campo _jwtoptions con las opciones de JWT proporcionadas.
		// IOptions<JwtOptions> es una interfaz de ASP.NET Core que facilita el acceso a las opciones de configuración.
		public JWTTokenGenerator(IOptions<JwtOptions> jwtoptions)
		{
			_jwtoptions = jwtoptions.Value;
		}



		string IJWTTokenGenerator.GenerateToken(IdentityUser user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			// Convierte la clave secreta configurada en _jwtoptions a un array de bytes.
			// La clave se utiliza para firmar el token y verificar su integridad.
			var key = Encoding.ASCII.GetBytes(_jwtoptions.Secret);

			// Define una lista de afirmaciones (claims) que se incluirán en el token.
			// Estas afirmaciones contienen información sobre el usuario y son utilizadas por el servidor para verificar la identidad del usuario.
			var claims = new List<Claim>
		{
				//Se guardan los campos introducidos por el usuario
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
			new Claim(JwtRegisteredClaimNames.Sub, user.Id),
			new Claim(JwtRegisteredClaimNames.Name, user.UserName)
		};

			// Configura las propiedades del token, incluyendo el público destinatario, el emisor,
			// las afirmaciones, la fecha de expiración y las credenciales para la firma del token.
			var tokenDescripter = new SecurityTokenDescriptor()
			{
				Audience = _jwtoptions.Audience,
				Issuer = _jwtoptions.Issuer,
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			// Crea el token basado en la configuración especificada en tokenDescriptor.
			var token = tokenHandler.CreateToken(tokenDescripter);

			// Convierte el token a un string y lo devuelve.
			return tokenHandler.WriteToken(token);
		}





	}
}
