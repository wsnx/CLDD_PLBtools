using System;
namespace AgilityRFtools
{
    class MainMenu:LoginForm
    {
        public static string storerkey;
        public static string FormName = "";
        public void Menu_Home()
        {
            Menu();
            void Menu()
                {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                string pilih;
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Welcome: "+ UserName);
                Console.WriteLine("__________________________________");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Pilihan : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1.Unloading Tools (Scan In )");
                Console.WriteLine("2.Mapping Checker (Scan Out)");
                Console.SetCursorPosition(0, 16);
                Console.Write("Back [esc] ");
                Console.Write("| Next [Enter]");
                Console.SetCursorPosition(0, 17);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("__________________________________");
                Console.WriteLine("    PLB Agility Tools v 2.0.1     ");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0,7);
                Console.Write("pilih menu : ");
                ConsoleKeyInfo rkey = Console.ReadKey();
                if (rkey.KeyChar == '1')
                {
                pilihan1();
                    Console.Clear();
                }
                if (rkey.KeyChar == '2')
                {

                    Console.Clear();
                    pilihan2();
                }
                
                if (rkey.KeyChar == '3')
                {
                
                }
                if(rkey.KeyChar == '0')
                {
                Console.Clear();

                }else if (rkey.Key ==ConsoleKey.Escape)
                {
                
                LoginForm c1 = new LoginForm();
                c1.FormLogin();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Pilihan salah !");
                    Console.ReadKey();
                    Menu();
                }
                
            }

        }
        private void onProses()
        {

            Console.SetCursorPosition(0,9);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Outstanding GR:");
            Console.WriteLine("12"+" ASN");
            Console.WriteLine("2700" + " Ctn");

        }
        private void pilihan1()
        {
            UnloadForm c1 = new UnloadForm();
            c1.Start();

        }
        private void pilihan2()
        {
            PalletChceking c1 = new PalletChceking();
            c1.Start();
        }

    }

}
