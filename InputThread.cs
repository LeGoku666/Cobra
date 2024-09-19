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
            bool interactionHandled = false;

            while (game.Running)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);

                    if (!interactionHandled)
                    {
                        game.NPCDialogue = "";
                    }
                    interactionHandled = false;

                    game.KeyPressed = key.KeyChar.ToString();

                    Direction newDirection = game.Player.FacingDirection;

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
                        case ConsoleKey.F:
                            HandleInteraction();
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
                            // Clear NPC dialogue when turning
                            game.NPCDialogue = "";
                        }
                        else
                        {
                            game.Player.Move(game.Board);
                            // Clear NPC dialogue when moving
                            game.NPCDialogue = "";
                        }
                    }
                }
                Thread.Sleep(50);
            }
        }

        private void HandleInteraction()
        {
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

            var npc = game.NPCs.FirstOrDefault(n => n.X == frontX && n.Y == frontY);
            if (npc != null)
            {
                // Display NPC dialogue
                game.NPCDialogue = npc.Name + ": " + npc.Dialogue;
            }
            else
            {
                // No NPC in front, clear dialogue
                game.NPCDialogue = "";
            }
        }
    }
}
