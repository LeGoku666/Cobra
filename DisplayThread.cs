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
                // Line 0: Game termination message
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Press ESC to end the game.");

                // Line 1: Information about the pressed key
                Console.SetCursorPosition(0, 1);
                if (!string.IsNullOrEmpty(game.KeyPressed))
                    Console.WriteLine("Key pressed: " + game.KeyPressed + "     ");
                else
                    Console.WriteLine("Key pressed:          ");

                // Line 2: Empty line
                Console.SetCursorPosition(0, 2);
                Console.WriteLine();

                // Line 3: Information about the tile the player is standing on
                Console.SetCursorPosition(0, 3);
                Tile currentTile = game.Board.GetTile(game.Player.X, game.Player.Y);
                Console.WriteLine("You are standing on: " + currentTile.Name + "       ");

                // Line 4: Information about what is in front of the player
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
                    Console.WriteLine("In front of you: " + frontTile.Name + "       ");
                }
                else
                {
                    Console.WriteLine("In front of you: out of bounds       ");
                }

                // Line 5: Information about the direction the player is facing
                Console.SetCursorPosition(0, 5);
                Console.WriteLine("Facing direction: " + game.Player.FacingDirection.ToString() + "     ");

                // Line 6: Empty line
                Console.SetCursorPosition(0, 6);
                Console.WriteLine();

                int mapStartLine = 7;
                Console.SetCursorPosition(0, mapStartLine);

                for (int y = 0; y < game.Board.Height; y++)
                {
                    for (int x = 0; x < game.Board.Width; x++)
                    {
                        if (game.Player.X == x && game.Player.Y == y)
                        {
                            Console.ForegroundColor = game.Player.Color;  // Use player's color
                            Console.Write(game.Player.Symbol + " ");
                        }
                        else
                        {
                            Tile tile = game.Board.Grid[y, x];
                            Console.ForegroundColor = tile.Color;  // Set tile color
                            Console.Write(tile.Symbol + " ");
                        }
                    }
                    Console.WriteLine();
                }

                // Reset console color after drawing the map
                Console.ResetColor();

                Thread.Sleep(delay);
            }

            // Display the game over message below the map
            Console.SetCursorPosition(0, 7 + game.Board.Height + 1);
            Console.WriteLine("Game Over.");
        }

    }
}
