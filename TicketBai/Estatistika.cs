using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace TicketBai
{
    internal class Estatistika
    {
        public static void SalmentaHandiena(string connString)
        {
            
            string query = @"
            SELECT s.Name, MAX(t.TotalAmount) AS MaxSale
            FROM tickets t
            JOIN sellers s ON t.SellerId = s.Id
            GROUP BY s.Name";

            using (MySqlConnection conn = new MySqlConnection(connString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Saltzailea: {reader["Name"]} | Salmenta handiena: {reader["MaxSale"]}€");
                }
            }
        }

        public static void BakoitzakZenbat(string connectionString)
        {
            string query = @"
            SELECT s.Name, COUNT(t.Id) AS TicketCount
            FROM tickets t
            JOIN sellers s ON t.SellerId = s.Id
            GROUP BY s.Name";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Saltzailea: {reader["Name"]} | Ticket kopurua: {reader["TicketCount"]}");
                }
            }
        }

        public static void EguneanZenbat(string connectionString)
        {
            string query = @"
            SELECT CAST(Date AS DATE) AS TicketDate, COUNT(Id) AS TicketCount
            FROM tickets
            GROUP BY CAST(Date AS DATE)
            ORDER BY TicketDate";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Eguna: {reader["TicketDate"]} | Ticketak: {reader["TicketCount"]}");
                }
            }
        }

        public static void ProduktuaTicketko(string connectionString)
        {
            string query = @"
            SELECT p.Name, COUNT(DISTINCT tl.TicketId) AS TicketFrequency
            FROM ticket_lines tl
            JOIN products p ON tl.ProductId = p.Id
            GROUP BY p.Name
            ORDER BY TicketFrequency DESC";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Produktua: {reader["Name"]} | Ticket desberdinak: {reader["TicketFrequency"]}");
                }
            }
        }

    }
}
