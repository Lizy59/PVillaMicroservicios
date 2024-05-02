using MicroservicioAuthenticationApi.DTO;
using MicroservicioAuthenticationApi.Model;
using MicroservicioAuthenticationApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioAuthenticationApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : Controller
	{
		private readonly IAuthentication _authentication;
		public AuthenticationController(IAuthentication authentication)
		{
			_authentication = authentication;	
		}
		[HttpPost]
		[Route("Login")]
		public async Task<ActionResult> Login([FromBody] LoginRequestDTO requestDTO)
		{
			ResultadoDTO resultDTO = new	ResultadoDTO();	
			var result = await _authentication.Login(requestDTO);
			if(result.UserDto == null)
			{
				resultDTO.Success = false;
				resultDTO.Message = "Nombre o contraseña incorrecto";
				return BadRequest(resultDTO);

			}
			resultDTO.Object = result;
			return Ok(resultDTO);
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDto)
		{
			ResultadoDTO resultDTO = new ResultadoDTO();
			var errorMessage = await _authentication.Register(registrationRequestDto);
			if (errorMessage != null)
			{
				resultDTO.Success = false;
				resultDTO.Message = errorMessage;
				return BadRequest(resultDTO);
			}
			else
			{
				return Ok(resultDTO);
			}

		}














	}
}
