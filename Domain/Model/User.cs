using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace APIPokemon.Domain.Model
{
    [Table("users")]
    public class User
    {
        [Key]
        public int user_id { get; private set; }
        public string username { get; private set; }
        public string password { get; private set; }

        public string? role { get; private set; }

        public User() { }
        public User(string username, string password, string role)
        {
            var password_crypto = new CriptoPassword();
            password = password_crypto.GetHashPassword(password);
            this.username = username;
            this.password = password;
            this.role = role;
        }
    }
}
