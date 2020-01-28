using System;

using System.Data.SqlClient;

namespace AgilityRFtools
{
    class LoginForm : ConfigDB
    {
        public static string FormType = "";
        private static string txt_NIK;
        private static string txt_Pass = "";
        public static string NIK;
        public static string UserName;
        public static string Password = "";
        public void FormLogin()
        {
        ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                               ");
            Console.WriteLine("      Agility International    ");
            Console.WriteLine("                               ");
            Console.WriteLine("_______________________________");
            Console.SetCursorPosition(0, 17);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("_______________________________");
            Console.WriteLine("   PLB Agility Tools v 2.0.1   ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(10, 5);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("                   ");
            Console.SetCursorPosition(10, 7);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("                   ");
            Console.SetCursorPosition(0, 5);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("NIK  :");
            Console.SetCursorPosition(0, 7);
            Console.Write("Pass :");
            Console.SetCursorPosition(10, 5);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            txt_NIK = Console.ReadLine();
            Console.SetCursorPosition(10, 7);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.Escape)
                {
                    goto ulang;
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    txt_Pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    Console.Write("\b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 8);
            Login();
        }
        private void Menu()
        {
            MainMenu L = new MainMenu();
            L.Menu_Home();
        }

        void Login()
        {

            SqlConnection cn = new SqlConnection(DBlocal);
            cn.Close();
            SqlCommand cmd = new SqlCommand("select NIK,UserName,Password from tbplbsami_fg_user where nik =@nik", cn);
            cmd.Parameters.AddWithValue("@NIK", txt_NIK);
            cn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    NIK = reader.GetString(0);
                    UserName = reader.GetString(1);
                    Password = reader.GetString(2);
                }
                ValidasiUser();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("NIK salah !");
                FormLogin();
            }
        }
        void ValidasiUser()
        {
            if (txt_Pass.Substring(0, 4) == Password)
            {
                Menu();
            }
            else
            {
                Console.ReadKey();
                Console.Clear();
                Console.SetCursorPosition(0, 8);
                Console.WriteLine("NIK SALAH");
                FormLogin();
            }
        }
    }
}
