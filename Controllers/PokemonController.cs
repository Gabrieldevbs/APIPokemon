using Microsoft.AspNetCore.Mvc;
using APIPokemon.Domain.Model;
using APIPokemon.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using APIPokemon.Application.ModelViews;

namespace APIPokemon.Controllers
{
    [ApiController]
    [Route("API/V1/Pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonController(IPokemonRepository pokemonRepository){
            _pokemonRepository = pokemonRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllPokemon()
        {
            return _pokemonRepository.GetAllPokemons() != null ? Ok(_pokemonRepository.GetAllPokemons()) : NotFound("Nenhum pokemon encontrado");
        }

        [Authorize]
        [HttpGet]
        [Route("id")]
        public IActionResult GetPokemon(int id)
        {
            return _pokemonRepository.GetPokemonById(id) != null ? Ok(_pokemonRepository.GetPokemonById(id)) : NotFound("Nenhum pokemon com esse ID encontrado");
        }

        [Authorize]
        [HttpGet]
        [Route("name")]
        public IActionResult GetPokemonByName(string name)
        {
            return _pokemonRepository.GetPokemonByName(name) != null ? Ok(_pokemonRepository.GetPokemonByName(name)) : NotFound("Nenhum pokemon com esse nome encontrado");
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeletePokemon(int id)
        {
            var pokemon = _pokemonRepository.GetPokemonById(id);
            var pokemonName = pokemon.FirstOrDefault()?.name;
            return _pokemonRepository.DeletePokemon(id) != false ? Ok(pokemonName + " foi deletado com sucesso!") : NotFound("Nenhum pokemon com esse ID encontrado");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddPokemon([FromForm] PokemonModelView pokemon)
        {
            var path = Path.Combine("Storage", pokemon.photo.FileName);

            using var stream = new FileStream(path, FileMode.Create);

            pokemon.photo.CopyTo(stream);
            var existingPokemon = new Pokemon(
                pokemon.name, pokemon.type1, pokemon.type2, pokemon.hp,
                pokemon.atk, pokemon.def, pokemon.satk, pokemon.sdef,
                pokemon.spd, pokemon.region, path
            );
            _pokemonRepository.AddPokemon(existingPokemon);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var pokemon = _pokemonRepository.DownloadPhoto(id);
            if (pokemon == null || pokemon.photo == null)
            {
                return NotFound("Nenhum pokemon com esse ID encontrado ou o pokemon não possui foto");
            }
            var filePath = pokemon.photo;
            var fileName = Path.GetFileName(filePath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/show")]
        public IActionResult ShowPhoto(int id)
        {
            var pokemon = _pokemonRepository.DownloadPhoto(id);
            if (pokemon == null || pokemon.photo == null)
            {
                return NotFound("Nenhum pokemon com esse ID encontrado ou o pokemon não possui foto");
            }
            var filePath = pokemon.photo;
            var fileName = Path.GetFileName(filePath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/png");
        }
    }
}