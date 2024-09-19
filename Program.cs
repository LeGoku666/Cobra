namespace Cobra
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set console encoding to UTF-8
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Hide cursor visibility
            Console.CursorVisible = false;

            Game game = new Game();
            game.Start();

            // Re-enable cursor visibility after the program ends
            Console.CursorVisible = true;
        }
    }
}
