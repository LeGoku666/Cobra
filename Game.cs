namespace Cobra
{
    public class Game
    {
        public bool Running { get; set; } = true;
        public string KeyPressed { get; set; } = "";

        public Board Board { get; set; }
        public Player Player { get; set; }
        public List<NPC> NPCs { get; set; } // List of NPCs

        public static int PlayerStartX { get; set; }
        public static int PlayerStartY { get; set; }

        public string NPCDialogue { get; set; } = "";  // To display NPC dialogue

        private Thread displayThread;
        private Thread inputThread;

        public Game()
        {
            string mapPath = "maps/map.bmp";  // Path to the map file
            Board = new Board(mapPath);
            Player = new Player(PlayerStartX, PlayerStartY);
            NPCs = new List<NPC>();
            InitializeNPCs();  // Initialize NPCs using positions from the map
        }

        private void InitializeNPCs()
        {
            foreach (var position in Board.NPCPositions)
            {
                // Create NPC at the specified position
                NPC npc = new NPC(position.X, position.Y, 'N', ConsoleColor.Yellow, "Guide", "Welcome to the game!");
                NPCs.Add(npc);
            }
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
