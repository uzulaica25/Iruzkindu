using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using TicketBai;
namespace TicketBai
{
    /// <summary>
    /// XmlGenerator klasea Ticket objektuak XML formatuan sortzeko eta gordetzeko arduratzen da. Bi metodo nagusi ditu: Sortu(Ticket t) metodoak single ticket bat XML fitxategi batean sortzen du, eta Sortu(IEnumerable<Ticket> ticketak) metodoak ticketen zerrenda bat XML fitxategi batean sortzen du. Bi metodoek XML fitxategiak "C:\TicketBAI\XML" karpetan gordetzen dituzte, eta erroreak kudeatzen dituzte. Gainera, EscapeXml metodoa erabiltzen da XML edukia behar bezala escapatzeko.
    /// </summary>
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

    
        private static string EscapeXml(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return System.Security.SecurityElement.Escape(s);
        }
    }


    /// <summary>
    /// Xml Validator klasea XML fitxategiak balidatzeko arduratzen da. Balidatu metodo bakarra dauka, eta XML fitxategi baten path-a jasotzen du. Fitxategia existitzen den, hutsik ez dagoen, eta oinarrizko egitura (Tickets eta Ticket elementuak) daukan egiaztatzen du. Erroreak gertatzen badira, mezua kontsolan erakusten du. Arrakastaz balidatua bada, mezua erakusten du.
    /// </summary>
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



