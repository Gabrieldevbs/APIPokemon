using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPokemon.Domain.Model
{
    [Table("favorite")]
    public class Favorite
    {
        [Key]
        public int id { get; private set; }
        public int databasepokemon_id { get; private set; }
        public int user_id { get; private set; }

        public Favorite() { }
        public Favorite(int databasepokemon_id, int user_Id)
        {
            this.databasepokemon_id = databasepokemon_id;
            this.user_id = user_Id;
        }
    }
}
