using APIPokemon.Domain.Model;
using APIPokemon.Domain.DTOs;
using APIPokemon.Application.Interfaces;

namespace APIPokemon.Infra.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly ConnectionContext _context;

        public PokemonRepository(ConnectionContext context)
        {
            _context = context;
        }

        public List<PokemonDTO> GetAllPokemons()
        {
            return _context.Pokemons.Select(p => new PokemonDTO()
            {
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
            }).ToList();
        }
        public List<PokemonDTO> GetPokemonById(int id)
        {
            return _context.Pokemons.Select(p => new PokemonDTO()
            {
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
            }).Where(p => p.pokemon_id == id).ToList();
        }
        public List<PokemonDTO> GetPokemonByName(string name)
        {
            return _context.Pokemons.Select(p => new PokemonDTO()
            {
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
            }).Where(p => p.name == name).ToList();
        }
        public void AddPokemon(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            _context.SaveChanges();
        }
        public void UpdatePokemon(Pokemon pokemon)
        {
            _context.Pokemons.Update(_context.Pokemons.FirstOrDefault(p => p.pokemon_id == pokemon.pokemon_id));
            _context.SaveChanges();
        }
        public bool DeletePokemon(int id)
        {   
            if (_context.Pokemons.FirstOrDefault(p => p.pokemon_id == id) == null)
            {
                return false;
            }
            _context.Pokemons.Remove(_context.Pokemons.FirstOrDefault(p => p.pokemon_id == id));
            _context.SaveChanges();
            return true;
        }

        public Pokemon? DownloadPhoto(int id)
        {
            var pokemon = _context.Pokemons.FirstOrDefault(p => p.pokemon_id == id);
            if (pokemon == null || pokemon.photo == null)
            {
                return null;
            }
            return pokemon;
        }
    }
}
