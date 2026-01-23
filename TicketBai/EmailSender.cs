using System;
using System.IO;
namespace TicketBai
{
    static class EmailSender
    {
        public static void Bidali(string xmlPath)
        {
            try
            {
                // Fitxategia existitzen dela egiaztatu
                if (string.IsNullOrWhiteSpace(xmlPath) ||!File.Exists(xmlPath))
                {
                    Console.WriteLine($"Errorea: XML fitxategia ez da aurkitu: {xmlPath}");
                    return;
                }

                // Simulazioa: email bidali balitz bezala
                Console.WriteLine($"Email bidalia: {xmlPath} (simulazioa)");

                // Benetan SMTP erabiliko bazenu:
                // - System.Net.Mail.MailMessage
                // - SmtpClient
                // - fitxategia gehitu attachment bezala
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea email bidaltzean: {ex.Message}");
            }
        }
    }
    static class ExcelLogger
    {
        private static string fitx = "bidalketak.csv";

        public static void Erregistratu(string ticketId, DateTime data)
        {
            try
            {
                bool sortuBerria = !File.Exists(fitx);

                using (StreamWriter sw = new StreamWriter(fitx, true)) // append=true
                {
                    // Burua sortu, fitxategia berria bada
                    if (sortuBerria)
                    {
                        sw.WriteLine("TicketID,Data");
                    }

                    // Ticket erregistratu
                    sw.WriteLine($"{ticketId},{data}");
                }

                Console.WriteLine($"Bidalketa erregistratua Excel/CSV-an: {ticketId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea Excel log-ean: {ex.Message}");
            }
        }
    }
}
