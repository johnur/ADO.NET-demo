using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace EkaAdoDemo
{
    class DA
    {
        private const string ConnString = "XXXX";
        public static void TulostaAsiakkaat()
        {
            // Kytkeydy tietokantaan eli luo SqlConnection-olio, määritä sille ConnectionStringja kutsu Open-metodia
            SqlConnection c = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand();
            c.Open();

            // Lisää SqlCommand-olio ja aseta sille sopiva sql-lause "select* fromasiakas orderbysukunimi"

            cmd.Connection = c;
            cmd.CommandText = "select * from asiakas order by sukunimi";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["asiakas_id"]} {reader["etunimi"]} {reader["sukunimi"]}");

            }

            reader.Close();
            c.Close();

        }
        public static void TulostaVasaroidenTilaajat()
        {
            // avaa tietokantayhteys kuten edellä(ja mieti miten voisit yhdistää koodia?)
            SqlConnection c = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand();
            c.Open();

            cmd.Connection = c;
            cmd.CommandText = @"select asiakas.asiakas_id, asiakas.etunimi, asiakas.sukunimi, SUM(tilausRivi.tilausLkm) as Vasaroidenmaara from asiakas
                join tilaus on asiakas.asiakas_id = tilaus.asiakas_id
                join tilausRivi on tilaus.tilaus_id = tilausRivi.tilaus_id
                join Tuote on tilausRivi.tuote_id = tuote.tuote_id
                where tuote.nimi = 'Vasara'
                group by asiakas.asiakas_id, asiakas.etunimi, asiakas.sukunimi ";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["sukunimi"]} {reader["Vasaroidenmaara"]}");

            }

            reader.Close();
            c.Close();

            //suorita sql-lause ja vinkkejä lauseen muodostamiseen löytyy SQL-harjoituksista.
            //tulosta asiakkaiden sukunimet ja montako vasaraa asiakas on ostanut 
            //tässä tarvitset siis tilausriviltä TilausLkm - saraketta 
            //Onnistuuko yhdellä sql - lauseella vai pitääkö sovelluksesta lähettää useita lauseita suoritettavaksi ? 

        }
        public static void TulostaTaulu (string taulu, int rivilkm = -1)
        {
            SqlConnection c = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand();
            c.Open();

            cmd.Connection = c;
            cmd.CommandText = $" select * from {taulu} order by 1";

            SqlDataReader reader = cmd.ExecuteReader();

          
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{reader.GetName(i)}\t");
            }
            Console.WriteLine();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                Console.Write($"{reader[i]}\t");
                }
                Console.WriteLine();
            }
            reader.Close();
            c.Close();

        }
        public static void TulostaTuotetyypit()
        {
            SqlConnection c = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand();
            c.Open();

            cmd.Connection = c;
            cmd.CommandText = "select tyyppi, sum(hinta) as summa from tuote group by tyyppi ";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["tyyppi"]} {reader["summa"]}");

            }

            reader.Close();
            c.Close();
        }

        public static void TulostaAsiakkaanTilaukset(int asiakas_id)
        {


            SqlConnection c = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand();
            c.Open();


            cmd.Connection = c;
            cmd.CommandText = @"select asiakas.sukunimi, asiakas.etunimi, tuote.nimi, tilausrivi.tilausLkm, tilaus.tilausPvm
                                from asiakas
                                inner join tilaus on Asiakas.asiakas_id = tilaus.asiakas_id
                                inner
                                join tilausrivi on tilaus.tilaus_id = TilausRivi.tilaus_id
                                inner
                                join tuote on tilausrivi.tuote_id = tuote.tuote_id where asiakas.asiakas_id = @asiakas_id order by tilauspvm";
                              

            cmd.Parameters.Add("@asiakas_id", SqlDbType.Int, 50);
            cmd.Parameters["@asiakas_id"].Value = asiakas_id;

            SqlDataReader reader = cmd.ExecuteReader();
          
            
            while (reader.Read())
            {
                Console.WriteLine($"{reader["etunimi"]} {reader["sukunimi"]}");
                

                while (reader.Read())
                {

                    if (true)
                    {
                        //Console.Write($"{reader.GetDateTime(3).ToString("d")}");
                        Console.WriteLine($" {reader["tilauslkm"]} {reader["nimi"]}");
                    }
                    else
                    {
                        
                    }
                  
                }
            }


            reader.Close();

            c.Close();
        }

    }
}
