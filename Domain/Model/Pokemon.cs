using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace APIPokemon.Domain.Model
{
    [Table("pokemon")]
    public class Pokemon
    {
        [Key]
        public int databasepokemon_id { get; private set; }
        public int pokemon_id { get; private set; }
        public string name { get; private set; }
        public string type1 { get; private set; }
        public string ?type2 { get; private set; }
        public int hp { get; private set; }
        public int atk { get; private set; }
        public int def { get; private set; }
        public int satk { get; private set; }
        public int sdef { get; private set; }
        public int spd { get; private set; }
        public string region { get; private set; }

        public string ?photo { get; private set; }


        public Pokemon(int pokemon_id, string name, string type1, string? type2, int hp, int atk, int def, int satk, int sdef, int spd, string region, string photo)
        {
            this.pokemon_id = pokemon_id;
            this.name = name;
            this.type1 = type1;
            this.type2 = type2;
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.satk = satk;
            this.sdef = sdef;
            this.spd = spd;
            this.region = region;
            this.photo = photo;
        }
    }
}
