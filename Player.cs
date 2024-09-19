namespace Cobra
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Player
    {
        public int X { get; set; }  // Position on the X axis (column)
        public int Y { get; set; }  // Position on the Y axis (row)
        public Direction FacingDirection { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }  // Added property

        public Player(int x, int y)
        {
            X = x;
            Y = y;
            FacingDirection = Direction.Up;  // Default direction
            Symbol = '^';  // Default symbol (facing up)
            Color = ConsoleColor.Cyan;  // Set player color
        }

        public void Turn(Direction newDirection)
        {
            FacingDirection = newDirection;

            // Update symbol based on direction
            switch (FacingDirection)
            {
                case Direction.Up:
                    Symbol = '^';
                    break;
                case Direction.Down:
                    Symbol = 'v';
                    break;
                case Direction.Left:
                    Symbol = '<';
                    break;
                case Direction.Right:
                    Symbol = '>';
                    break;
            }
        }

        public bool Move(Board board)
        {
            int deltaX = 0;
            int deltaY = 0;

            switch (FacingDirection)
            {
                case Direction.Up:
                    deltaY = -1;
                    break;
                case Direction.Down:
                    deltaY = 1;
                    break;
                case Direction.Left:
                    deltaX = -1;
                    break;
                case Direction.Right:
                    deltaX = 1;
                    break;
            }

            int newX = X + deltaX;
            int newY = Y + deltaY;

            // Check if the new position is within the bounds of the board
            if (newX < 0 || newX >= board.Width || newY < 0 || newY >= board.Height)
                return false;

            Tile tile = board.GetTile(newX, newY);

            // Check if the tile can be entered
            if (tile != null && tile.IsPassable)
            {
                X = newX;
                Y = newY;
                return true;
            }

            return false;
        }
    }
}
