
using APIPokemon.Domain.Model;
using APIPokemon.Domain.DTOs;

namespace APIPokemon.Application.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<FavoriteDTO>> GetAllFavorites(int id);
        Task<List<FavoriteDTO>> GetFavoriteByName(int user_id, string name);
        Task<bool> AddFavorite(Favorite favorite);
        Task<bool> DeleteFavorite(int user_id, int pokemon_id);
    }
}
