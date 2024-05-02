using MicroservicioAuthenticationApi.DTO;

namespace MicroservicioAuthenticationApi.Services.Interfaces
{
	public interface IAuthentication
	{
		public  Task<LoginResponseDTO> Login(LoginRequestDTO model);
		Task<string> Register(RegistrationRequestDTO registrationRequestDto);
	}
}
