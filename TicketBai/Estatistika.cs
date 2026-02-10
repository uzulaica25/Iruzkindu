using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBai
{
    internal class Estatistika
    {
        /// <summary>
        /// Saltzaile bakoitzaren salmenta handiena kalkulatzen du eta kontsolan erakusten du. SQL kontsulta bat exekutatzen du, non Ticket taula eta Saltzailea taula JOIN egiten diren, eta MAX funtzioa erabiltzen da prezio osoaren balio handiena lortzeko. Taldekatze bat egiten da saltzaile izenaren arabera, eta emaitzak kontsolan inprimatzen dira.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public static void SalmentaHandiena(string connString)
        {
            string query = @"
            SELECT s.SaltzaileaIzena, MAX(t.PrezioOsoa) AS SalmentaHandiena
            FROM Ticket t
            JOIN Saltzailea s ON t.idSaltzailea = s.idSaltzailea
            GROUP BY s.SaltzaileaIzena;
            ";
            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Saltzailea: {reader["SaltzaileaIzena"]} | Salmenta handiena: {reader["SalmentaHandiena"]}");
                }
            }
        }

        /// <summary>
        /// BakoitzakZenbat metodoak saltzaile bakoitzak zenbat ticket saldu dituen kalkulatzen du eta kontsolan erakusten du. SQL kontsulta bat exekutatzen du, non Ticket taula eta Saltzailea taula JOIN egiten diren, eta COUNT funtzioa erabiltzen da ticket kopurua lortzeko. Taldekatze bat egiten da saltzaile izenaren arabera, eta emaitzak kontsolan inprimatzen dira.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public static void BakoitzakZenbat(string connString)
        {
            string query = @"
            SELECT s.SaltzaileaIzena, COUNT(t.idTicket) AS TicketKopurua
            FROM Ticket t
            JOIN Saltzailea s ON t.idSaltzailea = s.idSaltzailea
            GROUP BY s.SaltzaileaIzena";

            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Saltzailea: {reader["SaltzaileaIzena"]} | Ticket kopurua: {reader["TicketKopurua"]}");
                }
            }
        }

        /// <summary>
        /// Egun bakoitzean sortutako ticket kopurua kalkulatzen du eta kontsolan erakusten du. SQL kontsulta bat exekutatzen du, non Ticket taula erabiltzen den, eta COUNT funtzioa erabiltzen da ticket kopurua lortzeko. Taldekatze bat egiten da ticket eguna (data) arabera, eta emaitzak kontsolan inprimatzen dira.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public static void EguneanZenbat(string connString)
        {
            string query = @"
            SELECT TicketEguna, COUNT(idTicket) AS TicketEgunean
            FROM Ticket
            GROUP BY TicketEguna
            ORDER BY TicketEguna";

            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Eguna: {reader["TicketEguna"]} | Ticketak: {reader["TicketEgunean"]}");
                }
            }
        }

        /// <summary>
        /// SalmentaMaiztasuna metodoak egun bakoitzean sortutako ticket-aren batez besteko salmenta kalkulatzen du eta kontsolan erakusten du. SQL kontsulta bat exekutatzen du, non Ticket taula erabiltzen den, eta AVG funtzioa erabiltzen da prezio osoaren batez bestekoa lortzeko. Taldekatze bat egiten da ticket eguna (data) arabera, eta emaitzak kontsolan inprimatzen dira.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public static void SalmentaMaiztasuna(string connString)
        {
            string query = @"
            SELECT DATE(t.TicketEguna) AS Eguna, AVG(t.PrezioOsoa) AS BatezBestekoSalmenta
            FROM Ticket t
            GROUP BY DATE(t.TicketEguna)
            ORDER BY Eguna ASC;
            ";
            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Eguna: {reader["Eguna"]} | Batez bestekoa: {reader["BatezBestekoSalmenta"]}");
                }
            }
        }

        }

    }

    

    


