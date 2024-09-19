namespace Cobra
{
    public class Tile
    {
        public string Name { get; set; }
        public char Symbol { get; set; }
        public bool IsPassable { get; set; }
        // Placeholder for additional effects
        // public Action<Player> OnEnter { get; set; }

        public Tile(string name, char symbol, bool isPassable)
        {
            Name = name;
            Symbol = symbol;
            IsPassable = isPassable;
        }
    }
}
