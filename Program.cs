using System;

namespace Cobra
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ustawienie kodowania konsoli na UTF-8
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Wyłączenie widoczności kursora
            Console.CursorVisible = false;

            Game game = new Game();
            game.Start();

            // Ponowne włączenie widoczności kursora po zakończeniu programu
            Console.CursorVisible = true;
        }
    }
}
