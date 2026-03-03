using APIPokemon.Domain.Model;
using APIPokemon.Domain.DTOs;
using APIPokemon.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace APIPokemon.Infra.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly ConnectionContext _context;

        public PokemonRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<List<PokemonDTO>> GetAllPokemons()
        {
            return await _context.Pokemons.Select(p => new PokemonDTO()
            {
                databasepokemon_id = p.databasepokemon_id,
                pokemon_id = p.pokemon_id,
                name = p.name,
                type1 = p.type1,
                type2 = p.type2,
                hp = p.hp,
                atk = p.atk,
                def = p.def,
                satk = p.satk,
                sdef = p.sdef,
                spd = p.spd,
                region = p.region,
                photo = p.photo
            }).ToListAsync();
        }
        public async Task<List<PokemonDTO>> GetPokemonById(int id)
        {
            return await _context.Pokemons.Select(p => new PokemonDTO()
            {
                databasepokemon_id = p.databasepokemon_id,
                pokemon_id = p.pokemon_id,
                name = p.name,
                type1 = p.type1,
                type2 = p.type2,
                hp = p.hp,
                atk = p.atk,
                def = p.def,
                satk = p.satk,
                sdef = p.sdef,
                spd = p.spd,
                region = p.region,
                photo = p.photo
            }).Where(p => p.pokemon_id == id).ToListAsync();
        }
        public async Task<List<PokemonDTO>> GetPokemonByName(string name)
        {
            return await _context.Pokemons.Select(p => new PokemonDTO()
            {
                databasepokemon_id = p.databasepokemon_id,
                pokemon_id = p.pokemon_id,
                name = p.name,
                type1 = p.type1,
                type2 = p.type2,
                hp = p.hp,
                atk = p.atk,
                def = p.def,
                satk = p.satk,
                sdef = p.sdef,
                spd = p.spd,
                region = p.region,
                photo = p.photo
            }).Where(p => p.name.Contains(name)).ToListAsync();
        }
        public void AddPokemon(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            _context.SaveChanges();
        }
        public void UpdatePokemon(Pokemon pokemon)
        {
            _context.Pokemons.Update(pokemon);
            _context.SaveChanges();
        }
        public async Task<bool> DeletePokemon(int id)
        {   
            if (_context.Pokemons.FirstOrDefault(p => p.databasepokemon_id == id) == null)
            {
                return false;
            }
            _context.Pokemons.Remove(_context.Pokemons.FirstOrDefault(p => p.pokemon_id == id));
            _context.SaveChanges();
            return true;
        }

        public async Task<Pokemon> DownloadPhoto(int id)
        {
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.pokemon_id == id);
            if (pokemon == null || pokemon.photo == null)
            {
                return null;
            }
            return pokemon;
        }
    }
}
