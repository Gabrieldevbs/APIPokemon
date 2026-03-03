using APIPokemon.Domain.Model;
using APIPokemon.Domain.DTOs;
using APIPokemon.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIPokemon.Infra.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ConnectionContext _context;

        public FavoriteRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<List<FavoriteDTO>> GetAllFavorites(int user_id)
        {
            return await _context.Favorites.Select(f => new FavoriteDTO() {
                id = f.id,
                user_id = f.user_id,
                databasepokemon_id = f.databasepokemon_id
            }).Where(f => f.user_id == user_id).ToListAsync();
        }
        public async Task<List<FavoriteDTO>> GetFavoriteByName(int user_id, string name)
        {
            if (_context.Pokemons.FirstOrDefault(p => p.name == name) == null)
            {
                return null;
            }

            var id_pokemon = _context.Pokemons.FirstOrDefault(p => p.name.Contains(name)).pokemon_id;

            if (_context.Favorites.Where(u => u.user_id == user_id).FirstOrDefault(f => f.databasepokemon_id == id_pokemon) == null)
            {
                return null;
            }
            return await _context.Favorites.Select(f => new FavoriteDTO() {  
                id = f.id, 
                user_id = f.user_id,
                databasepokemon_id = f.databasepokemon_id
            }).Where(u => u.user_id == user_id && u.databasepokemon_id == id_pokemon).ToListAsync();
        }
        public async Task<bool> AddFavorite(Favorite favorite){
            
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.databasepokemon_id == favorite.databasepokemon_id);

            if (pokemon == null)
            {
                return false;
            }

            var favorite_exists = await _context.Favorites.Where(u => u.user_id == favorite.user_id).FirstOrDefaultAsync(f => f.databasepokemon_id == favorite.databasepokemon_id);
            if (favorite_exists != null)
            {
                return false;
            }
            _context.Favorites.Add(favorite);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteFavorite(int user_id, int pokemon_id)
        {
            if (_context.Favorites.Where(u => u.user_id == user_id).FirstOrDefault(f => f.databasepokemon_id == pokemon_id) == null)
            {
                return false;
            }
            _context.Favorites.Remove(_context.Favorites.Where(u => u.user_id == user_id).FirstOrDefault(f => f.databasepokemon_id == pokemon_id));
            _context.SaveChanges();
            return true;
        }
    }
}
