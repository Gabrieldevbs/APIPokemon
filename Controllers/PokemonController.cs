using APIPokemon.Application.Interfaces;
using APIPokemon.Application.ModelViews;
using APIPokemon.Domain.DTOs;
using APIPokemon.Domain.Model;
using APIPokemon.Infra.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllPokemon()
        {
            return await _pokemonRepository.GetAllPokemons() != null ? Ok(await _pokemonRepository.GetAllPokemons()) : NotFound("Nenhum pokemon encontrado");
        }

        [Authorize]
        [HttpGet]
        [Route("{pokemon_id}")]
        public async Task<IActionResult> GetPokemon(int pokemon_id)
        {
            if (pokemon_id == null) { 
                return NotFound("Digite o ID do pokemon.");
            }
            return await _pokemonRepository.GetPokemonById(pokemon_id) != null ? Ok(await _pokemonRepository.GetPokemonById(pokemon_id)) : NotFound("Nenhum pokemon com esse ID encontrado");
        }

        [Authorize]
        [HttpGet]
        [Route("name")]
        public async Task<IActionResult> GetPokemonByName(string name)
        {
            if (name == null)
            {
                return NotFound("Digite o nome do pokemon.");
            }
            return await _pokemonRepository.GetPokemonByName(name) != null ? Ok(await _pokemonRepository.GetPokemonByName(name)) : NotFound("Nenhum pokemon com esse nome encontrado");
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeletePokemon(int pokemon_id)
        {
            if (pokemon_id == null)
            {
                return NotFound("Digite o ID do pokemon.");
            }
            var pokemon = await _pokemonRepository.GetPokemonById(pokemon_id);
            var pokemonName = pokemon.FirstOrDefault()?.name;
            return await _pokemonRepository.DeletePokemon(pokemon_id) != false ? Ok(pokemonName + " foi deletado com sucesso!") : NotFound("Nenhum pokemon com esse ID encontrado");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPokemon([FromForm] PokemonModelView pokemon)
        {
            var extension = Path.GetExtension(pokemon.photo.FileName).ToLower();

            if (extension != ".png")
            {
                return BadRequest("Apenas arquivos .png são permitidos.");
            }
    
            var path = Path.Combine("Storage", pokemon.photo.FileName);

            using var stream = new FileStream(path, FileMode.Create);

            pokemon.photo.CopyTo(stream);
            var existingPokemon = new Pokemon(pokemon.pokemon_id,
                pokemon.name, pokemon.type1, pokemon.type2, pokemon.hp,
                pokemon.atk, pokemon.def, pokemon.satk, pokemon.sdef,
                pokemon.spd, pokemon.region, path
            );
            return await _pokemonRepository.AddPokemon(existingPokemon) != false ? Ok("Pokemon adicionado com sucesso!") : BadRequest("Erro ao adicionar pokemon");  
        }

        [Authorize]
        [HttpPost]
        [Route("{pokemon_id}/download")]
        public async Task<IActionResult> DownloadPhoto(int pokemon_id)
        {
            if (pokemon_id == null)
            {
                return NotFound("Digite o ID do pokemon.");
            }
            var pokemon = await _pokemonRepository.DownloadPhoto(pokemon_id);
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
        [Route("{pokemon_id}/show")]
        public async Task<IActionResult> ShowPhoto(int pokemon_id)
        {
            if (pokemon_id == null)
            {
                return NotFound("Digite o ID do pokemon.");
            }
            var pokemon = await _pokemonRepository.DownloadPhoto(pokemon_id);
            if (pokemon == null || pokemon.photo == null)
            {
                return NotFound("Nenhum pokemon com esse ID encontrado ou o pokemon não possui foto");
            }
            var filePath = pokemon.photo;
            var fileName = Path.GetFileName(filePath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/png");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePokemon([FromForm] Pokemon pokemon)
        {
            var pokemon_is_valid = await _pokemonRepository.GetPokemonById(pokemon.pokemon_id);
            if (pokemon_is_valid == null)
            {
                return NotFound("Nenhum Pokemon encontrado com esse ID para atualizar");
            }
            _pokemonRepository.UpdatePokemon(pokemon);
            return Ok("Usuário foi atualizado");
        }
    }
}