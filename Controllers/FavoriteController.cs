using APIPokemon.Application.Interfaces;
using APIPokemon.Application.ModelViews;
using APIPokemon.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace APIPokemon.Controllers
{
    [ApiController]
    [Route("API/V1/Favorites")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IPokemonRepository _pokemonRepository;

        public FavoriteController(IFavoriteRepository favoriteRepository, IPokemonRepository pokemonRepository)
        {
            _favoriteRepository = favoriteRepository;
            _pokemonRepository = pokemonRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromForm]FavoriteModelView favoriteModelView)
        {
            var favorite = new Favorite(favoriteModelView.databasepokemon_id, int.Parse(User.FindFirst("id")?.Value));
            return await _favoriteRepository.AddFavorite(favorite) != false ? Ok("Pokemon adicionado aos favoritos com sucesso!") : BadRequest("Erro ao adicionar pokemon aos favoritos!");
            
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllFavorites()
        {
            var user_id = int.Parse(User.FindFirst("id")?.Value);
            var favorites = await _favoriteRepository.GetAllFavorites(user_id);
            return Ok(favorites);
        }

        [Authorize]
        [HttpGet]
        [Route("Name")]
        public async Task<IActionResult> FavoritebyName(string name)
        {
            if (name == null)
            {
                return NotFound("Digite o nome.");
            }
            var user_id = int.Parse(User.FindFirst("id")?.Value);
            var favorites = await _favoriteRepository.GetFavoriteByName(user_id, name);
            if (favorites == null)
            {
                return NotFound("Não existe esse favorito!");
            }
            return Ok(favorites);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteFavorite(int pokemon_id)
        {
            if (pokemon_id == null)
            {
                return NotFound("Digite o ID.");
            }
            var user_id = int.Parse(User.FindFirst("id")?.Value);
            var pokemon = await _pokemonRepository.GetPokemonById(pokemon_id);
            if (pokemon == null)
            {
                return NotFound("Nenhum pokemon com esse ID encontrado");
            }
            var pokemonName = pokemon.FirstOrDefault().name;
            var database_id_pokemon = pokemon.FirstOrDefault().databasepokemon_id;
            return await _favoriteRepository.DeleteFavorite(user_id, database_id_pokemon) != false ? Ok(pokemonName + " foi deletado dos favoritos com sucesso!") : NotFound("Nenhum pokemon com esse ID encontrado nos favoritos");

        }
    }
}
