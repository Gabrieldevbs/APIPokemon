
using APIPokemon.Domain.Model;
using APIPokemon.Domain.DTOs;

namespace APIPokemon.Application.Interfaces
{
    public interface IFavoriteRepository
    {
        List<FavoriteDTO> GetAllFavorites(int id);
        List<FavoriteDTO> GetFavoriteByName(int user_id, string name);
        bool AddFavorite(Favorite favorite);
        bool DeleteFavorite(int user_id, int pokemon_id);
    }
}
