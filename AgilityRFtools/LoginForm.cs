using System;
using System.Drawing;
using Console = Colorful.Console;


namespace AgilityRFtools
{
    class LoginForm
    {
        public static string NIK;
        public static string UserName;


        public void FormLogin()
        {
            Console.WindowTop = 0;
            Console.BackgroundColor = Color.White;
            Console.ForegroundColor = Color.Black;
            Console.Clear();
            Console.SetCursorPosition(1, 10);
            Console.Write("Back [esc] ");
            Console.Write("| Next [Enter]");
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = Color.DarkOrange;
            Console.ForegroundColor = Color.Black;
            Console.WriteLine("                                        ");
            Console.WriteLine("          Agility International         ");
            Console.WriteLine("                                        ");
            Console.BackgroundColor = Color.White;
            Console.SetCursorPosition(0,5);
            Console.Write("Entri NIK   :");
            Console.SetCursorPosition(0, 6);
            Console.Write("Password    :");
            Console.SetCursorPosition(14,5);
            NIK = Console.ReadLine();
            Console.SetCursorPosition(14, 6);
            UserName = Console.ReadLine();

            Menu();    
        }
        private void Menu()
        {
            UnloadForm L = new UnloadForm();
            L.FormHeader();

        }
    }
}
