using APIPokemon.Application.ModelViews;
using APIPokemon.Domain.Model;
using APIPokemon.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIPokemon.Controllers
{
    [ApiController]
    [Route("API/V1/Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult AddUser([FromForm] UserModelView userview)
        {
            var user = new User(
                userview.Username, userview.Password, "User"
            );

            _userRepository.AddUser(user);
            return Ok("User criado com sucesso!");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return await _userRepository.GetAllUsers() != null ? Ok(await _userRepository.GetAllUsers()) : NotFound("Nenhum User encontrado");
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            var userName = user?.username;
            return await _userRepository.DeleteUser(id) != false ? Ok(userName + " foi deletado com sucesso") : NotFound("Nenhum User encontrado com esse ID para deletar");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] User user)
        {
            var user_is_valid = await _userRepository.GetUserById(user.user_id);
            if (user_is_valid == null)
            {
                return NotFound("Nenhum User encontrado com esse ID para atualizar");
            }
            _userRepository.UpdateUser(user);
            return Ok("Usuário foi atualizado");
        }

        [Authorize]
        [HttpGet("Logado")]
        public IActionResult GetUserinSystem()
        {
            var userid = User.FindFirst("id")?.Value;
            var username = User.FindFirst("username")?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                userid,
                username,
                role
            });
        }
    }
    }
