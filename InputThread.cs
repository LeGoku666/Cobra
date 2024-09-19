using System;
using System.Threading;

namespace Cobra
{
    public class InputThread
    {
        private Game game;

        public InputThread(Game game)
        {
            this.game = game;
        }

        public void Run()
        {
            while (game.Running)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);

                    game.KeyPressed = key.KeyChar.ToString();

                    Direction newDirection = game.Player.FacingDirection;
                    bool directionChanged = false;

                    switch (key.Key)
                    {
                        case ConsoleKey.W:
                            newDirection = Direction.Up;
                            break;
                        case ConsoleKey.S:
                            newDirection = Direction.Down;
                            break;
                        case ConsoleKey.A:
                            newDirection = Direction.Left;
                            break;
                        case ConsoleKey.D:
                            newDirection = Direction.Right;
                            break;
                        case ConsoleKey.Escape:
                            game.Running = false;
                            break;
                    }

                    if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.S || key.Key == ConsoleKey.A || key.Key == ConsoleKey.D)
                    {
                        if (game.Player.FacingDirection != newDirection)
                        {
                            game.Player.Turn(newDirection);
                        }
                        else
                        {
                            game.Player.Move(game.Board);
                        }
                    }
                }
                Thread.Sleep(50);
            }
        }
    }
}
