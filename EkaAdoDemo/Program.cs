using System;
using System.Data.SqlClient;

namespace EkaAdoDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            // DA.TulostaAsiakkaat();
            //DA.TulostaVasaroidenTilaajat();
            //DA.TulostaTaulu("Asiakas", 5);
            //DA.TulostaTuotetyypit();
            DA.TulostaAsiakkaanTilaukset(50);



           /* SqlConnection c = new SqlConnection();
            c.ConnectionString = "database=MyyntiDB;Server=DESKTOP-F5NJUA7\\MSSQLSERVER01;trusted_connection=true";
            c.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = c;
            cmd.CommandText = "select * from tuote where tuote_id < 5";

            decimal summa = 0M;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["nimi"]);
                Console.WriteLine(reader[1]);
                summa += reader.GetDecimal(4);

            }
            Console.WriteLine("summa: " + summa);
            reader.Close();
            c.Close(); */

        }
    }
}
