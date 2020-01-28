using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilityRFtools
{
    class CycleCounted
    {
        private static string txt_Doc="";
        private static string txt_Loc="";
        private static string txt_PalletID="";
        private static string strKey="";
        private static string key="";
        public void Start()
        {
            Head();
            FormHeader();
            F_DocNo();
        }
        public void Head()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            MainMenu.FormName = "StokTake";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("             Stok Take              ");
            Console.WriteLine("____________________________________");
            Console.ForegroundColor = ConsoleColor.Green;
        }
        private void backtomenu()
        {

        }
        private void Handler()
        {
            if (key=="0")
            {
                FormHeader();
                F_DocNo();
            }
            else if (key=="1")
            {
                FormHeader();
                f_Loc();
            }
            else if (key=="2")
            {

            }

        }


        public void FormHeader()
        {
            Head();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Doc. No        : "); //1
            Console.WriteLine("Loc            : ");//2
            Console.WriteLine("Actual LPN     : ");//3
            Console.WriteLine("Scan QR        : ");//4
            Console.ForegroundColor = ConsoleColor.White;

            //Value
            Console.SetCursorPosition(17, 2);
            Console.WriteLine(txt_Doc);
            Console.SetCursorPosition(17, 3);
            Console.WriteLine(txt_Loc);
            Console.SetCursorPosition(17, 4);
            Console.WriteLine(txt_PalletID);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Total :");
            Console.SetCursorPosition(11, 8);
            Console.WriteLine("OF ");
            Console.SetCursorPosition(18, 8);
            Console.WriteLine("CTN");
            Console.SetCursorPosition(0, 9);

        }
        private void F_DocNo()
        {
            Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("                    ");
            Console.SetCursorPosition(16, 2);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape)
                {
                    backtomenu();
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
                }
                else
                 if (cki.Key == ConsoleKey.Enter)
                {
                    if (strKey == "")
                    {
                        txt_Doc = "";
                        goto Ulang;
                    }
                    else
                    {
                        txt_Doc = strKey;
                        key = "1";
                        Handler();


                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }

        }

        private void f_Loc()
        {
            Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 3);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("                    ");
            Console.SetCursorPosition(16, 3);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    key = "0";
                    Handler();
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
                }
                else
                 if (cki.Key == ConsoleKey.Enter)
                {
                    if (strKey == "")
                    {
                        txt_Loc = "";
                        goto Ulang;
                    }
                    else
                    {
                        txt_Loc = strKey;
                        key = "2";
                        Handler();

                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }
            throw new NotImplementedException();
        }
        private void f_PalletID()
        {
            Ulang:
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(16, 4);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("                    ");
            Console.SetCursorPosition(16, 4);
            ConsoleKeyInfo cki;
            strKey = "";
            while (true)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.UpArrow)
                {
                    key = "2";
                    Handler();
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    goto Ulang;
                }
                else
                 if (cki.Key == ConsoleKey.Enter)
                {
                    if (strKey == "")
                    {
                        txt_PalletID = "";
                        goto Ulang;
                    }
                    else
                    {
                        txt_PalletID = strKey;
                        key = "3";
                        Handler();

                    }
                }
                else
                {
                    strKey = strKey + cki.KeyChar;
                }
            }

            throw new NotImplementedException();
        }
    }
}
