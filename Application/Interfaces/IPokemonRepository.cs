using APIPokemon.Domain.Model; 
using APIPokemon.Domain.DTOs;

namespace APIPokemon.Application.Interfaces
{
    public interface IPokemonRepository
    {
        List<PokemonDTO> GetAllPokemons();
        List<PokemonDTO> GetPokemonById(int id);
        List<PokemonDTO> GetPokemonByName(string name);
        void AddPokemon(Pokemon pokemon);
        void UpdatePokemon(Pokemon pokemon);
        bool DeletePokemon(int id);

        Pokemon? DownloadPhoto(int id);
    }
}
