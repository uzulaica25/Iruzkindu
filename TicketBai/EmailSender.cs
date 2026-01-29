using System;
using System.IO;
using System.Net;
using System.Net.Mail;
namespace TicketBai
{
    static class EmailSender
    {
        public static void Bidali(string xmlPath)
        {
            string xmlKarpeta = @"C:\TicketBAI\XML";

            string[] xmlFitxategiak = Directory.GetFiles(xmlKarpeta);

            if (xmlFitxategiak.Length == 0)
            {
                Console.WriteLine("⚠ Ez da XML fitxategirik aurkitu");
                return;
            }

            // 3️⃣ XML bakoitza bidali
            foreach (string fitxategi in xmlFitxategiak)
            {
                try
                {
                    MailMessage test = new MailMessage("unaxzulaila@gmail.com", "unaxzulaila@gmail.com");
                    test.Subject = "XMl ";
                    test.Body = "Hemen daude salmenten tiketak";

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("unaxzulaila@gmail.com", "njao trwe rzbl yuoe ");
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;

                    smtp.Send(test);
                    Console.WriteLine("✅ Test mezua bidali da!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Test mezua bidaltzean: " + ex.Message);
                }

                Console.WriteLine("✅ Prozesua amaitu da");
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

