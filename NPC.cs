namespace Cobra
{
    public class NPC
    {
        public int X { get; set; }  // Position on the X axis (column)
        public int Y { get; set; }  // Position on the Y axis (row)
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }
        public string Name { get; set; }
        public string Dialogue { get; set; }

        public NPC(int x, int y, char symbol, ConsoleColor color, string name, string dialogue)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            Color = color;
            Name = name;
            Dialogue = dialogue;
        }
    }
}
