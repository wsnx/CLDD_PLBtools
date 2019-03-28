using System;
using CLDD_PLBtools.PLBSAI001;
using CLDD_PLBtools.PLBKMS001;
namespace CLDD_PLBtools
{
    class MainMenu : LoginForm
    {
        public static string storerkey;
        public void Menu_Home()
        {
            Menu();
               
                void Menu()
                {
                    Console.Clear();
                    string pilih;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(UserName);
                    Console.WriteLine("_____________________________");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Pilihan Customer : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("1.PLB SAI T");
                    Console.WriteLine("2.PLB JAI");
                    Console.WriteLine("3.PLB SAI B");
                    Console.WriteLine("4.PLB SAMI");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("pilih menu : ");
                    ConsoleKeyInfo rkey = Console.ReadKey();
                    if (rkey.KeyChar == '1')
                    {
                        Console.Clear();
                        storerkey = "PLBSAI001";
                        PLBSAI001();
                    }
                    if (rkey.KeyChar == '2')
                    {

                        Console.Clear();
                        storerkey = "PLBJAI001";
                        PLBSAI001();
                    }
                    
                    if (rkey.KeyChar == '3')
                    {
                        Console.Clear();
                        storerkey = "PLBSAB001";
                        PLBSAI001();
                    }
                    if(rkey.KeyChar == '0')
                {

                    LogOut();
                    Console.Clear();

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
        void PLBSAI001 ()
        {

            PLBSAI001_Home c2 = new PLBSAI001_Home();
            c2.Home();
        }
        void LogOut()
        {

            LoginForm c2 = new LoginForm();
            c2.FormLogin();
        }
        void Komatsu()
        {
            PLBKMS001_Home c2 = new PLBKMS001_Home();
            c2.Home();
        }
        void PickingSummary()
        {
            Picking c2 = new Picking();
            c2.PickingMenu();
        }
        void OutboundMenu()
        {
            Outbounds c2 = new Outbounds();
            c2.Home();
        }
        void Inventory()
        {
            Inventory c2 = new Inventory();
            c2.Home();
        }
    }
    
}
