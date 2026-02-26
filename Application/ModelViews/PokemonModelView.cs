namespace APIPokemon.Application.ModelViews
{
    public class PokemonModelView
    {
        public string name { get; set; }
        public string type1 { get; set; }
        public string? type2 { get; set; }
        public int hp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int satk { get; set; }
        public int sdef { get; set; }
        public int spd { get; set; }
        public string region { get; set; }
        public IFormFile photo { get; set; }
    }
}
