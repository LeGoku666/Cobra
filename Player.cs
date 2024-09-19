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
        public int X { get; set; }  // Pozycja na osi X (kolumna)
        public int Y { get; set; }  // Pozycja na osi Y (wiersz)
        public Direction FacingDirection { get; set; }
        public char Symbol { get; set; }

        public Player(int x, int y)
        {
            X = x;
            Y = y;
            FacingDirection = Direction.Up;  // Domyślny kierunek
            Symbol = '^';  // Domyślny symbol (patrząc w górę)
        }

        public void Turn(Direction newDirection)
        {
            FacingDirection = newDirection;

            // Aktualizacja symbolu w zależności od kierunku
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

            // Sprawdzenie, czy nowa pozycja jest w granicach planszy
            if (newX < 0 || newX >= board.Width || newY < 0 || newY >= board.Height)
                return false;

            Tile tile = board.GetTile(newX, newY);

            // Sprawdzenie, czy można wejść na dany kafelek
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
