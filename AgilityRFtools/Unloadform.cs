using System;
using System.Data.SqlClient;
using System.Drawing;

namespace AgilityRFtools
{

    class UnloadForm
    {
        private static string ASN;
        private static string CartonID;
        private static string Carline;
        private static string Conn = "Integrated Security=False;Data Source=10.130.24.4;Initial Catalog=KaizenDB;User ID=kadmin;Password=53c4dm1n;";
        private static string NIK = "18.332";
        private static string UserName="" ;
        private static string PalletID="";
        private static string SKU="";
        private static int Qty=10;
        private static DateTime Editdate = DateTime.Now;
        private static string key;


        public void Frame()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("     Form Unloading     ");
            Console.BackgroundColor = ConsoleColor.White;

        }
        public void FormHeader ()
        {
            Frame();

            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("Carline Type :");

            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("ASN No.      :");

            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("PalletID     :");

            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("Scan QR      :");
            Handler();
        }

        public static void ClearLine()
        {
        int currentLeft = Console.CursorLeft;
        int currentTop = Console.CursorTop;
        Console.Write(new String(' ', Console.WindowWidth - currentLeft));
        Console.SetCursorPosition(currentLeft, currentTop);
        }

        private static void ScanResult()
        {


        }
        public void Handler()
        {

            if (key=="")
            {
                

            }
            else if (key =="1")
            {
                Frame();
                Console.SetCursorPosition(14, 2);
                Console.Write(ASN);
                Console.SetCursorPosition(14, 3);
                Console.Write(Carline);
                Console.SetCursorPosition(14, 4);
                Console.Write(PalletID);
                Console.SetCursorPosition(14, 5);
                Console.Write(CartonID);
                

            }
            else if (key =="2")
            {

            }
            else if(key =="3")
            {

            }
            else
            {
                Console.SetCursorPosition(14, 2);
                Carline = Console.ReadLine();
                Console.SetCursorPosition(14, 3);
                ASN = Console.ReadLine();
                Console.SetCursorPosition(14, 4);
                PalletID = Console.ReadLine();
                Console.SetCursorPosition(14, 5);
                CartonID = Console.ReadLine();
                key = "1";
                Frame();

            }
        }
        private void ParsingQR()
        {

            UpdateSql();
        }

        void UpdateSql()
        {
            SqlConnection cn = new SqlConnection(Conn);
            cn.Close();
            SqlCommand cmd = new SqlCommand("INSERT INTO kaizenDB.dbo.tbPLBSAMI_FG_UnloadingDetail(" +
                "ASN,CarlineType,SKU,CartonID,Qty,PalletID,EditDate,UserName,NIK)" +
                " VALUES (@ASN,@Carline,@SKU,@CartonID,@Qty,@PalletID,@EditDate,@UserName,@NIK)", cn);
            cmd.Parameters.Add(new SqlParameter("ASN", ASN));
            cmd.Parameters.Add(new SqlParameter("Carline",Carline ));
            cmd.Parameters.Add(new SqlParameter("SKU", SKU));
            cmd.Parameters.Add(new SqlParameter("CartonID", CartonID));
            cmd.Parameters.Add(new SqlParameter("Qty", Qty));
            cmd.Parameters.Add(new SqlParameter("PalletID", PalletID));
            cmd.Parameters.Add(new SqlParameter("EditDate", Editdate));
            cmd.Parameters.Add(new SqlParameter("UserName", UserName));
            cmd.Parameters.Add(new SqlParameter("NIK", NIK));
            cn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                cn.Close();
                key = "1";
                Handler();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
                cn.Close();
            }
        }

        private static void resultSql()
        {
            
            SqlConnection cn = new SqlConnection(Conn);
            SqlCommand cmd = new SqlCommand("select ASN,CartonID,PalletID from tbPLBSAMI_FG_UnloadingDetail" +
                "  order by transid desc", cn);
            //cmd.Parameters.AddWithValue("@ASN",ASN );
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0} \t|{1} ",
                    reader[1], reader[2]));
                }

                reader.Close();
            Console.Read();
            
        }

    }
}