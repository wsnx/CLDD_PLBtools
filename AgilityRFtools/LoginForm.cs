using System;


namespace AgilityRFtools
{
    class LoginForm
    {
        public static string NIK;
        public static string UserName;

        public static string Password="";

        public void FormLogin()
        {
            ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WindowTop = 0;
            Console.Clear();

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                  ");
            Console.WriteLine("       Agility International      ");
            Console.WriteLine("                                  ");
            Console.WriteLine("__________________________________");

            Console.SetCursorPosition(0, 15);
            Console.Write("Back [esc] ");
            Console.Write("| Next [Enter]");

            Console.SetCursorPosition(0, 18);
            Console.WriteLine("__________________________________");
            Console.WriteLine("   PLB Agility Tools v 2.0.1      ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(10, 5);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("                     ");
            Console.SetCursorPosition(10, 7);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("                     ");
            Console.SetCursorPosition(0,5);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("NIK  :");            
            Console.SetCursorPosition(0, 7);
            Console.Write("Pass :");
            Console.SetCursorPosition(10, 5);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            NIK = Console.ReadLine();
            Console.SetCursorPosition(10, 7);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow|| key.Key == ConsoleKey.Escape)
                {
                    goto ulang;
                }
                else if(key.Key != ConsoleKey.Backspace)
                {
                    Password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    Console.Write("\b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            Console.BackgroundColor = ConsoleColor.Black;
            Menu(); 
            
        }
        private void Menu()
        {
            Trial L = new Trial();
            L.test();

        }
    }
}
