using APIPokemon.Domain.Model;
using APIPokemon.Domain.DTOs;

namespace APIPokemon.Application.Interfaces
{
    public interface IUserRepository
    {
            List<UsersDTO> GetAllUsers();
            UsersDTO GetUserById(int id);
            void AddUser(User user);
            void UpdateUser(User user);
            bool DeleteUser(int id);
            User GetUser(string username, string password);

    }
}
