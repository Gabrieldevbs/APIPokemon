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

        [Authorize]
        [HttpPost]
        public IActionResult AddUser([FromForm] UserModelView userview)
        {
            var user = new User(
                userview.Username, userview.Password, "User"
            );

            _userRepository.AddUser(user);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return _userRepository.GetAllUsers() != null ? Ok(_userRepository.GetAllUsers()) : NotFound("Nenhum User encontrado");
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            var userName = user?.username;
            return _userRepository.DeleteUser(id) != false ? Ok(userName + " foi deletado com sucesso") : NotFound("Nenhum User encontrado com esse ID para deletar");
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateUser([FromForm] User user)
        {
            _userRepository.UpdateUser(user);
            return Ok();
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
