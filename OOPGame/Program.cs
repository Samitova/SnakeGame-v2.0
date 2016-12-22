using NConsoleGraphics;
using System;


namespace OOPGame
{       
    public class Program
    {
        static void Main(string[] args)
        {
            SetConsole();
            SnakeGameEngine game = new SnakeGameEngine(new ConsoleGraphics());
            game.Start();
        
        }

        /// <summary>
        /// Prepare consol
        /// </summary>
        private static void SetConsole()
        {
            Console.SetWindowSize(50, 50);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.BackgroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            Console.Clear();
        }
    }
}
