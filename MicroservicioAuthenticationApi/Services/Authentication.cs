using MicroservicioAuthenticationApi.DTO;
using MicroservicioAuthenticationApi.Model;
using MicroservicioAuthenticationApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MicroservicioAuthenticationApi.Services
{
	public class Authentication : IAuthentication //implementacion de interfaces
	{
		private readonly AppDbContext _context;
		private readonly UserManager<IdentityUser> _userManager; // Gestor de usuarios para operaciones relacionadas con usuarios.
		private readonly RoleManager<IdentityRole> _roleManager; // Gestor de roles para operaciones relacionadas con roles.
		private readonly IJWTTokenGenerator _jwtTokenGenerator; // Generador de tokens JWT para crear tokens para usuarios.

		// Constructor que inicializa las dependencias inyectadas.


		public Authentication(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IJWTTokenGenerator jwtTokenGenerator)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		public async Task<LoginResponseDTO> Login(LoginRequestDTO model)
		{
			LoginResponseDTO responseDTO = new LoginResponseDTO();
			var usuario =_context.Users.SingleOrDefault(c => c.UserName.ToLower() == model.UserName.ToLower());
			if(usuario != null)
			{

				// Verifica la contraseña del usuario.
				bool esValida = await _userManager.CheckPasswordAsync(usuario, model.Password);
				if (esValida) // Si la contraseña es correcta
				{
					// Genera un token JWT para el usuario.
					var token = _jwtTokenGenerator.GenerateToken(usuario);

					// Crea un DTO de usuario para incluirlo en la respuesta.
					UsuarioDTO usuarioDTO = new UsuarioDTO
					{
						UserName = usuario.UserName,
						IdUsuario = usuario.Id,
						Numero = usuario.PhoneNumber,
						Email = usuario.Email
					};

					// Configura la respuesta con el token y la información del usuario.
					responseDTO.Token = token;
					responseDTO.UserDto = usuarioDTO;
				}
				else // Si la contraseña no es correcta, devuelve una respuesta sin token ni información del usuario.
				{
					responseDTO.Token = "";
					responseDTO.UserDto = null;
				}
			}
			else // Si no se encuentra el usuario, devuelve una respuesta sin token ni información del usuario.
			{
				responseDTO.Token = "";
				responseDTO.UserDto = null;
			}
			return responseDTO;
		}

		// Método asincrónico Register que toma un DTO de solicitud de registro y devuelve un string.
		// Este método registra un nuevo usuario en el sistema.
		public async Task<string> Register(RegistrationRequestDTO registrationRequestDto)
		{
			// Crea un nuevo objeto IdentityUser con la información proporcionada.
			IdentityUser user = new IdentityUser
			{
				UserName = registrationRequestDto.UserName,
				Email = registrationRequestDto.Email,
				NormalizedEmail = registrationRequestDto.Email.ToUpper(),
				PhoneNumber = registrationRequestDto.Numero
			};

			try
			{
				// Intenta crear el usuario en el sistema con la contraseña proporcionada.
				var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
				if (result.Succeeded) // Si la creación es exitosa
				{
					// Busca el usuario recién creado para obtener su información.
					var userResult = _context.Users.First(u => u.UserName == registrationRequestDto.UserName);

					// Crea un DTO de usuario (aunque no se utiliza posteriormente en este método).
					UsuarioDTO userDto = new UsuarioDTO
					{
						UserName = userResult.UserName,
						Email = userResult.Email,
						IdUsuario = userResult.Id,
						Numero = userResult.PhoneNumber
					};

					// Retorna una cadena vacía indicando éxito (podría ser más útil devolver algún identificador o mensaje de éxito).
					return null;
				}
				else // Si la creación falla, retorna el primer error encontrado.
				{
					return result.Errors.First().Description;
				}
			}
			catch (Exception ex) // Captura excepciones generales, pero no las maneja ni registra.
			{

			}

			// Si se produce una excepción no manejada, devuelve "Error".
			return "Error";
		}



	}
}
