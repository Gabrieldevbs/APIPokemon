using APIPokemon.Application.ModelViews;
using APIPokemon.Domain.Model;
using APIPokemon.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult AddFavorite([FromForm]FavoriteModelView favoriteModelView)
        {
            var pokemon = _pokemonRepository.GetPokemonById(favoriteModelView.pokemon_id);
            if (pokemon == null) {
                return NotFound("Pokemon não encontrado!");
            }
            var favorite = new Favorite(favoriteModelView.pokemon_id, int.Parse(User.FindFirst("id")?.Value));
            return _favoriteRepository.AddFavorite(favorite) != false ? Ok("Pokemon adicionado aos favoritos com sucesso!") : BadRequest("Erro ao adicionar pokemon aos favoritos!");
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllFavorites()
        {
            var user_id = int.Parse(User.FindFirst("id")?.Value);
            var favorites = _favoriteRepository.GetAllFavorites(user_id);
            return Ok(favorites);
        }

        [Authorize]
        [HttpGet]
        [Route("Name")]
        public IActionResult FavoritebyName(string name)
        {
            var user_id = int.Parse(User.FindFirst("id")?.Value);
            var favorites = _favoriteRepository.GetFavoriteByName(user_id, name);
            if (favorites == null)
            {
                return NotFound("Não existe esse favorito!");
            }
            return Ok(favorites);
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteFavorite(int pokemon_id)
        {
            var user_id = int.Parse(User.FindFirst("id")?.Value);
            var pokemon = _pokemonRepository.GetPokemonById(pokemon_id);
            var pokemonName = pokemon.FirstOrDefault()?.name;
            return _favoriteRepository.DeleteFavorite(user_id, pokemon_id) != false ? Ok(pokemonName + " foi deletado dos favoritos com sucesso!") : NotFound("Nenhum pokemon com esse ID encontrado nos favoritos");

        }
    }
}
