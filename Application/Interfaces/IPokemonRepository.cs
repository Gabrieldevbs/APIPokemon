using APIPokemon.Domain.Model; 
using APIPokemon.Domain.DTOs;

namespace APIPokemon.Application.Interfaces
{
    public interface IPokemonRepository
    {
        Task<List<PokemonDTO>> GetAllPokemons();
        Task<List<PokemonDTO>> GetPokemonById(int id);
        Task<List<PokemonDTO>> GetPokemonByName(string name);
        Task<bool> AddPokemon(Pokemon pokemon);
        void UpdatePokemon(Pokemon pokemon);
        Task<bool> DeletePokemon(int id);

        Task<Pokemon> DownloadPhoto(int id);
    }
}
