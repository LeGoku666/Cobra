namespace Cobra
{
    public class UpdateThread
    {
        private Game game;

        public UpdateThread(Game game)
        {
            this.game = game;
        }

        public void Run()
        {
            while (game.Running)
            {
                // In this implementation, the board is updated in real-time by the Move method in the Player class,
                // so we do not need to update it here additionally.
                Thread.Sleep(50);
            }
        }
    }
}
