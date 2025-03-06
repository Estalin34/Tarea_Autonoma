namespace Tarea_Autonoma.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<PokemonTypeWrapper> Types { get; set; }
        public PokemonSprites Sprites { get; set; }
    }

    public class PokemonTypeWrapper
    {
        public PokemonType Type { get; set; }
    }

    public class PokemonType
    {
        public string Name { get; set; }
    }

    public class PokemonSprites
    {
        public string Front_Default { get; set; }
    }
}
