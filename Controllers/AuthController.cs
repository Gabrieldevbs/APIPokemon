using Microsoft.AspNetCore.Mvc;
using APIPokemon.Application.Interfaces;
using APIPokemon.Infra.Services;

namespace APIPokemon.Controllers
{
    [ApiController]
    [Route("API/V1/Auth")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public AuthController(IUserRepository userRepository, TokenService tokenService) {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Auth(string username, string password)
        {
            var user = await _userRepository.GetUser(username, password);

            if (user != null)
            {
                var token = _tokenService.GenerateToken(user);
                return Ok("Seu token: " + token);
            }

            return Unauthorized("Seu username ou senha não conferem!");

        }
    }
}
