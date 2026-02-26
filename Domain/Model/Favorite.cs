using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPokemon.Domain.Model
{
    [Table("favorite")]
    public class Favorite
    {
        [Key]
        public int id { get; private set; }
        public int pokemon_id { get; private set; }
        public int user_id { get; private set; }

        public Favorite() { }
        public Favorite(int pokemon_Id, int user_Id)
        {
            this.pokemon_id = pokemon_Id;
            this.user_id = user_Id;
        }
    }
}
