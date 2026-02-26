using APIPokemon.Domain.DTOs;
using APIPokemon.Domain.Model;
using APIPokemon.Application.Interfaces;
namespace APIPokemon.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionContext _context;

        public UserRepository(ConnectionContext context)
        {
            _context = context;
        }

        public List<UsersDTO> GetAllUsers()
        {
            return _context.Users.Select(u => new UsersDTO() { user_id = u.user_id, username = u.username}).ToList();
        }

        public UsersDTO GetUserById(int id)
        {
            return _context.Users.Select(u => new UsersDTO() { user_id = u.user_id, username = u.username }).FirstOrDefault(u => u.user_id == id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
        public bool DeleteUser(int id)
        {
            if (_context.Users.FirstOrDefault(u => u.user_id == id) == null)
            {
                return false;
            }
            _context.Users.Remove(_context.Users.FirstOrDefault(u => u.user_id == id));
            _context.SaveChanges();
            return true;
        }

        public User GetUser(string username, string password)
        {
            var password_crypto = new CriptoPassword();
            var criptopass = password_crypto.GetHashPassword(password);
            return _context.Users.FirstOrDefault(u => u.username == username && u.password == criptopass);
            
        }
    }
}
