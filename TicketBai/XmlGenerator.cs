using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using TicketBai;
namespace TicketBai
{
    static class XmlGenerator
    {
        
        public static string Sortu(Ticket t)
        {
            string karpeta = @"C:\TicketBAI\XML";
            Directory.CreateDirectory(karpeta);
            string xmlPath = Path.Combine(karpeta, $"{t.Id}.xml");

            try
            {
                using (StreamWriter sw = new StreamWriter(xmlPath, false, Encoding.UTF8))
                {
                    sw.WriteLine($"<Ticket id='{t.Id}'>");
                    sw.WriteLine($"  <Data>{EscapeXml($"{t.Eguna} {t.Ordua}")}</Data>");
                    sw.WriteLine($"  <Saltzailea>{EscapeXml(t.Saltzailea?.Izena)}</Saltzailea>");
                    sw.WriteLine($"  <Guztira>{t.PrezioOsoa}</Guztira>");
                    if (t.Produktuak != null && t.Produktuak.Count > 0)
                    {
                        sw.WriteLine("  <Produktuak>");
                        foreach (var p in t.Produktuak)
                        {
                            sw.WriteLine("    <Produktua>");
                            sw.WriteLine($"      <Izena>{EscapeXml(p.Izena)}</Izena>");
                            sw.WriteLine($"      <Prezioa>{p.Prezioa}</Prezioa>");
                            sw.WriteLine("    </Produktua>");
                        }
                        sw.WriteLine("  </Produktuak>");
                    }
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

        
        public static string Sortu(IEnumerable<Ticket> ticketak)
        {
            string karpeta = @"C:\TicketBAI\XML";
            Directory.CreateDirectory(karpeta);

            string xmlPath = Path.Combine(karpeta, "Ticketak.xml");

            try
            {
                using (StreamWriter sw = new StreamWriter(xmlPath, false, Encoding.UTF8))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    sw.WriteLine("<Tickets>");

                    foreach (var t in ticketak)
                    {
                        sw.WriteLine($"  <Ticket id='{t.Id}'>");
                        sw.WriteLine($"    <Data>{EscapeXml($"{t.Eguna} {t.Ordua}")}</Data>");
                        sw.WriteLine($"    <Saltzailea>{EscapeXml(t.Saltzailea?.Izena)}</Saltzailea>");
                        sw.WriteLine($"    <Guztira>{t.PrezioOsoa}</Guztira>");

                        if (t.Produktuak != null && t.Produktuak.Count > 0)
                        {
                            sw.WriteLine("    <Produktuak>");
                            foreach (var p in t.Produktuak)
                            {
                                sw.WriteLine("      <Produktua>");
                                sw.WriteLine($"        <Izena>{EscapeXml(p.Izena)}</Izena>");
                                sw.WriteLine($"        <Prezioa>{p.Prezioa}</Prezioa>");
                                sw.WriteLine("      </Produktua>");
                            }
                            sw.WriteLine("    </Produktuak>");
                        }

                        sw.WriteLine("  </Ticket>");
                    }

                    sw.WriteLine("</Tickets>");
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

        // Escapa contenido para XML
        private static string EscapeXml(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return System.Security.SecurityElement.Escape(s);
        }
    }

    static class XmlValidator
    {
        public static bool Balidatu(string xmlPath)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlPath) || !File.Exists(xmlPath))
                {
                    Console.WriteLine($"XML fitxategia ez da aurkitu: {xmlPath}");
                    return false;
                }

                string content = File.ReadAllText(xmlPath).Trim();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine($"XML fitxategia hutsik dago: {xmlPath}");
                    return false;
                }

                if (!content.Contains("<Tickets") || !content.Contains("</Tickets>") || !content.Contains("<Ticket"))
                {
                    Console.WriteLine($"XML baliogabea: egitura ez dator bat: {xmlPath}");
                    return false;
                }

                Console.WriteLine("XML baliozkoa da XSDaren arabera.");
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



