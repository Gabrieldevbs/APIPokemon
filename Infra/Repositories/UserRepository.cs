using APIPokemon.Domain.DTOs;
using APIPokemon.Domain.Model;
using APIPokemon.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace APIPokemon.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionContext _context;

        public UserRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<List<UsersDTO>> GetAllUsers()
        {
            return await _context.Users.Select(u => new UsersDTO() { user_id = u.user_id, username = u.username}).ToListAsync();
        }

        public async Task<UsersDTO> GetUserById(int id)
        {
            return await _context.Users.Select(u => new UsersDTO() { user_id = u.user_id, username = u.username }).FirstOrDefaultAsync(u => u.user_id == id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public async Task<bool> DeleteUser(int id)
        {
            if (_context.Users.FirstOrDefault(u => u.user_id == id) == null)
            {
                return false;
            }
            _context.Users.Remove(_context.Users.FirstOrDefault(u => u.user_id == id));
            _context.SaveChanges();
            return true;
        }

        public async Task<User> GetUser(string username, string password)
        {
            var password_crypto = new CriptoPassword();
            var criptopass = password_crypto.GetHashPassword(password);
            return await _context.Users.FirstOrDefaultAsync(u => u.username == username && u.password == criptopass);
            
        }
    }
}
