using APIPokemon.Domain.Model;
using APIPokemon.Domain.DTOs;

namespace APIPokemon.Application.Interfaces
{
    public interface IUserRepository
    {
            Task<List<UsersDTO>> GetAllUsers();
            Task<UsersDTO> GetUserById(int id);
            void AddUser(User user);
            void UpdateUser(User user);
            Task<bool> DeleteUser(int id);
            Task<User> GetUser(string username, string password);

    }
}
