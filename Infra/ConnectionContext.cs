using APIPokemon.Domain.Model;
using APIPokemon.Infra.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace APIPokemon.Infra
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

    }
}
