using System.Threading;

namespace Cobra
{
    public class Game
    {
        public bool Running { get; set; } = true;
        public string KeyPressed { get; set; } = "";

        public Board Board { get; set; }
        public Player Player { get; set; }

        public static int PlayerStartX { get; set; }
        public static int PlayerStartY { get; set; }

        private Thread displayThread;
        private Thread updateThread;
        private Thread inputThread;

        public Game()
        {
            string mapPath = "map.bmp";  // Ścieżka do pliku mapy
            Board = new Board(mapPath);
            Player = new Player(PlayerStartX, PlayerStartY);
        }

        public void Start()
        {
            displayThread = new Thread(() => new DisplayThread(this).Run());
            inputThread = new Thread(() => new InputThread(this).Run());

            displayThread.Start();
            inputThread.Start();

            while (Running)
            {
                Thread.Sleep(100);
            }

            displayThread.Join();
            inputThread.Join();
        }
    }
}
