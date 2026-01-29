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

    

    


