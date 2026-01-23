using System;
using System.IO;
using TicketBai;
namespace TicketBai
{
    static class XmlGenerator
    {
        public static string Sortu(Ticket t)
        {
            // Fitxategiaren izena definitu
            string xmlPath = $"{t.Id}.xml";

            try
            {
                using (StreamWriter sw = new StreamWriter(xmlPath))
                {
                    sw.WriteLine($"<Ticket id='{t.Id}'>");
                    sw.WriteLine($"  <Data>{t.Data}</Data>");
                    sw.WriteLine($"  <Saltzailea>{t.Saltzailea.Izena}</Saltzailea>");
                    sw.WriteLine($"  <Guztira>{t.KalkulatuTotala()}</Guztira>");
                    sw.WriteLine("  <Produktuak>");
                    foreach (var p in t.Produktuak)
                    {
                        sw.WriteLine($"    <Produktua izena='{p.Izena}' prezioa='{p.Prezioa}' />");
                    }
                    sw.WriteLine("  </Produktuak>");
                    sw.WriteLine("</Ticket>");
                }

                Console.WriteLine($"XML sortua: {xmlPath}");
                return xmlPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea XML sortzean: {ex.Message}");
                return null;
            }
        }
    }
    static class XmlValidator
    {
        public static bool Balidatu(string xmlPath)
        {
            try
            {
                // Fitxategia existitzen dela egiaztatu
                if (!File.Exists(xmlPath))
                {
                    Console.WriteLine($"XML fitxategia ez da aurkitu: {xmlPath}");
                    return false;
                }

                // Fitxategi hutsik ez dagoela egiaztatu
                string content = File.ReadAllText(xmlPath).Trim();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine($"XML fitxategia hutsik dago: {xmlPath}");
                    return false;
                }

                // Simple check: <Ticket> tag-a badago
                if (!content.Contains("<Ticket") || !content.Contains("</Ticket>"))
                {
                    Console.WriteLine($"XML baliogabea: <Ticket> tag-a falta da: {xmlPath}");
                    return false;
                }

                // Balidazioa pasatu
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errorea XML balidazioan: {ex.Message}");
                return false;
            }
        }
    }
}

