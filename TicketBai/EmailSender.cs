using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml;
using System.Xml.Schema;
namespace TicketBai
{
    static class EmailSender
    {
        public static void Bidali()
        {
            string karpeta = @"C:\TicketBAI\XML";
            string jaso = "tickfy28@gmail.com";

            string[] xmlak = Directory.GetFiles(karpeta, "*.xml");

            if (xmlak.Length == 0)
                throw new Exception("Ez dago .xml fitxategirik karpeta honetan: " + karpeta);

            string xmlBidea = xmlak[0];

            Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
            if (outlookType == null)
                throw new Exception("Outlook ez dago instalatuta ordenagailu honetan.");

            dynamic outlookApp = Activator.CreateInstance(outlookType);
            dynamic mail = outlookApp.CreateItem(0);

            mail.To = jaso;
            mail.Subject = "Ticketak XML formatuan";
            mail.Body = "Kaixo, hemen dago XML-a gure 4 baskulek egindako ticketekin";
            mail.Attachments.Add(xmlBidea);

            mail.Send();


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


