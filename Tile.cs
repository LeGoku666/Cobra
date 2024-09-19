namespace Cobra
{
    public class Tile
    {
        public string Name { get; set; }
        public char Symbol { get; set; }
        public bool IsPassable { get; set; }
        public ConsoleColor Color { get; set; }  // Added property

        public Tile(string name, char symbol, bool isPassable, ConsoleColor color)
        {
            Name = name;
            Symbol = symbol;
            IsPassable = isPassable;
            Color = color;  // Assign color
        }
    }
}
