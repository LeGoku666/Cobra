using System;
using System.Threading;

namespace Cobra
{
    public class DisplayThread
    {
        private Game game;
        private int delay;

        public DisplayThread(Game game)
        {
            this.game = game;
            delay = 1000 / 14;  // 14 FPS
        }

        public void Run()
        {
            while (game.Running)
            {
                // Linia 0: Komunikat o zakończeniu gry
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Naciśnij ESC, aby zakończyć grę.");

                // Linia 1: Informacja o naciśniętym klawiszu
                Console.SetCursorPosition(0, 1);
                if (!string.IsNullOrEmpty(game.KeyPressed))
                    Console.WriteLine("Naciśnięto klawisz: " + game.KeyPressed + "     ");
                else
                    Console.WriteLine("Naciśnięto klawisz:          ");

                // Linia 2: Pusta linia
                Console.SetCursorPosition(0, 2);
                Console.WriteLine();

                // Linia 3: Informacja o tym, na jakim kafelku stoi gracz
                Console.SetCursorPosition(0, 3);
                Tile currentTile = game.Board.GetTile(game.Player.X, game.Player.Y);
                Console.WriteLine("Stoisz na: " + currentTile.Name + "       ");

                // Linia 4: Informacja o tym, co jest przed graczem
                Console.SetCursorPosition(0, 4);
                int frontX = game.Player.X;
                int frontY = game.Player.Y;

                switch (game.Player.FacingDirection)
                {
                    case Direction.Up:
                        frontY -= 1;
                        break;
                    case Direction.Down:
                        frontY += 1;
                        break;
                    case Direction.Left:
                        frontX -= 1;
                        break;
                    case Direction.Right:
                        frontX += 1;
                        break;
                }

                Tile frontTile = game.Board.GetTile(frontX, frontY);

                if (frontTile != null)
                {
                    Console.WriteLine("Przed tobą: " + frontTile.Name + "       ");
                }
                else
                {
                    Console.WriteLine("Przed tobą: poza mapą       ");
                }

                // Linia 5: Informacja o kierunku patrzenia gracza
                Console.SetCursorPosition(0, 5);
                Console.WriteLine("Kierunek patrzenia: " + game.Player.FacingDirection.ToString() + "     ");

                // Linia 6: Pusta linia
                Console.SetCursorPosition(0, 6);
                Console.WriteLine();

                int mapStartLine = 7;
                Console.SetCursorPosition(0, mapStartLine);

                for (int y = 0; y < game.Board.Height; y++)
                {
                    for (int x = 0; x < game.Board.Width; x++)
                    {
                        if (game.Player.X == x && game.Player.Y == y)
                            Console.Write(game.Player.Symbol + " "); // Dodajemy spację
                        else
                            Console.Write(game.Board.Grid[y, x].Symbol + " "); // Dodajemy spację
                    }
                    Console.WriteLine();
                }

                Thread.Sleep(delay);
            }

            // Wyświetlenie komunikatu o zakończeniu gry poniżej mapy
            Console.SetCursorPosition(0, 7 + game.Board.Height + 1);
            Console.WriteLine("Gra zakończona.");
        }

    }
}
