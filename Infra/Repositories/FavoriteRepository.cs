using APIPokemon.Domain.Model;
using APIPokemon.Domain.DTOs;
using APIPokemon.Application.Interfaces;

namespace APIPokemon.Infra.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ConnectionContext _context;

        public FavoriteRepository(ConnectionContext context)
        {
            _context = context;
        }

        public List<FavoriteDTO> GetAllFavorites(int user_id)
        {
            return _context.Favorites.Select(f => new FavoriteDTO() { 
                id = f.id, 
                user_id = f.user_id, 
                pokemon_id = f.pokemon_id
            }).Where(f => f.user_id == user_id).ToList();
        }
        public List<FavoriteDTO> GetFavoriteByName(int user_id, string name)
        {
            if (_context.Pokemons.FirstOrDefault(p => p.name == name) == null)
            {
                return null;
            }

            var id_pokemon = _context.Pokemons.FirstOrDefault(p => p.name == name).pokemon_id;

            if (_context.Favorites.Where(u => u.user_id == user_id).FirstOrDefault(f => f.pokemon_id == id_pokemon) == null)
            {
                return null;
            }
            return _context.Favorites.Select(f => new FavoriteDTO() { 
                id = f.id, 
                user_id = f.user_id, 
                pokemon_id = f.pokemon_id }).Where(u => u.user_id == user_id && u.pokemon_id == id_pokemon).ToList();
        }
        public bool AddFavorite(Favorite favorite){
            _context.Favorites.Add(favorite);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteFavorite(int user_id, int pokemon_id)
        {
            if (_context.Favorites.Where(u => u.user_id == user_id).FirstOrDefault(f => f.pokemon_id == pokemon_id) == null)
            {
                return false;
            }
            _context.Favorites.Remove(_context.Favorites.Where(u => u.user_id == user_id).FirstOrDefault(f => f.pokemon_id == pokemon_id));
            _context.SaveChanges();
            return true;
        }
    }
}
