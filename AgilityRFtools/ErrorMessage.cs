using System;

namespace AgilityRFtools
{
    class ErrorMessage
    {
        private static string key;

        public void Start()
        {

        }
        private void Display()
        {
            Console.SetCursorPosition(0, 12);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("╔═══════════════════════╗");
            Console.WriteLine("║     Press Esc...      ║");
            Console.WriteLine("╚═══════════════════════╝");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

        ulang:
            string strkey = "";
            ConsoleKeyInfo cki;
            cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Escape)
            {

                Handler(key);
            }

            else
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write(" ");
                Console.SetCursorPosition(0, currentLineCursor);
                goto ulang;
            }
        }
        private void Handler(string key)
        {
        }

    }
}
