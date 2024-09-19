using System.Threading;

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
                // W tej implementacji plansza jest aktualizowana w czasie rzeczywistym przez metodę Move w klasie Player,
                // więc nie musimy jej tutaj dodatkowo aktualizować.
                Thread.Sleep(50);
            }
        }
    }
}
