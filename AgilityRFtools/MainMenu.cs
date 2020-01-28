using System;
namespace AgilityRFtools
{
    class MainMenu : LoginForm
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Welcome: " + UserName);
                Console.WriteLine("__________________________________");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Pilihan : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1.Unload Checking (Scan In )");//Existing Form 
                Console.WriteLine("2.Mapping Checking(Scan Out)");
                Console.WriteLine("3.Picking Checking");
                Console.WriteLine("4.Loading Checking");
                Console.WriteLine("7.Scan Docking");
                Console.WriteLine("8.Dyna (Docking)");
                Console.WriteLine("9.Cek Label");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("6.Unload Checking (Scan In ) [BETA]"); //Update Form : Fix Error
                Console.SetCursorPosition(0, 16);
                Console.Write("Back [esc] ");
                Console.Write("| Next [Enter]");
                Console.SetCursorPosition(0, 17);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("__________________________________");
                Console.WriteLine("        PLB Tools (SAMI FG)       ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0, 13);
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
                    Console.Clear();
                    pilihan3();
                }
                if (rkey.KeyChar == '4')
                {
                    Console.Clear();
                    pilihan4();
                }
                if (rkey.KeyChar == '5')
                {
                    Console.Clear();
                    CycleCounted c1 = new CycleCounted();
                    c1.Start();
                }
                if (rkey.KeyChar == '6')
                {
                    Console.Clear();
                    UnloadFormV1 c1 = new UnloadFormV1();
                    c1.Start();
                }
                if (rkey.KeyChar == '0')
                {
                    Console.Clear();

                }
                if (rkey.KeyChar == '7')
                {

                    Console.Clear();
                    CekPallet c1 = new CekPallet();
                    c1.Start();

                }
                if (rkey.KeyChar == '8')
                {

                    Console.Clear();
                    Dyna c1 = new Dyna();
                    c1.Start();

                }

                if (rkey.KeyChar == '9')
                {

                    Console.Clear();
                    CekLabel c1 = new CekLabel();
                    c1.Start();

                }
                else if (rkey.Key == ConsoleKey.Escape)
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

            Console.SetCursorPosition(0, 9);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Outstanding GR:");
            Console.WriteLine("12" + " ASN");
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
        private void pilihan3()
        {
            OutbundChecking c1 = new OutbundChecking();
            c1.start();
        }
        private void pilihan4()
        {
            LoadingChecking_v2 c1 = new LoadingChecking_v2();
            c1.Start();
        }
    }

}
