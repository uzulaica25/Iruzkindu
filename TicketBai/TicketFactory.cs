using System;
using System.Collections.Generic;
using System.IO;
using TicketBai;


static class TicketFactory
{
    
    private static int nextId = 1;

    /// <summary>
    /// Ticketak sortzeko metodoa. Lerroak, baskula eta txt fitxategiaren bidea jasotzen ditu. Lerro bakoitza prozesatzen du, produktua, saltzailea, prezioa eta pisua ateratzen ditu, eta Ticket objektuak sortzen ditu. Ticket bakoitzari baskula esleitzen dio, eta sortutako ticket guztiak itzultzen ditu. Prozesatzeko lerroak formatu okerrean badaude, mezua kontsolan erakusten du eta lerroa saltatzen du. Arrakastaz sortutako ticket bakoitza kontsolan erakusten du.
    /// </summary>
    /// <param name="lerroak">The lerroak.</param>
    /// <param name="baskula">The baskula.</param>
    /// <param name="txtPath">The text path.</param>
    /// <returns></returns>
    public static List<Ticket> SortuTicketak(List<string> lerroak, string baskula, string txtPath)
    {
        List<Ticket> ticketak = new List<Ticket>();

        
        string fileName = Path.GetFileNameWithoutExtension(txtPath);
        DateTime fechaTicket = DateTime.Now; 
        string[] partes = fileName.Split('_');
        if (partes.Length >= 2)
        {
            if (!DateTime.TryParse(partes[1], out fechaTicket))
            {
                Console.WriteLine("Data okerra " + partes[1]);
                fechaTicket = DateTime.Now;
            }
        }

        foreach (string lerroa in lerroak)
        {
            if (string.IsNullOrWhiteSpace(lerroa))
                continue;

            string[] zatitu = lerroa.Split('$'); 

            if (zatitu.Length < 5)
            {
                Console.WriteLine("Formatu okerra: " + lerroa);
                continue;
            }

            
            string produktuaIzena = zatitu[0];
            string saltzaileaId = zatitu[1].Trim();

            string saltzaileaIzena;
            switch (saltzaileaId)
            {
                case "001": saltzaileaIzena = "Lander"; break;
                case "002": saltzaileaIzena = "Ander"; break;
                case "003": saltzaileaIzena = "Mateo"; break;
                case "004": saltzaileaIzena = "Unai"; break;
                case "005": saltzaileaIzena = "Eber"; break;
                case "006": saltzaileaIzena = "Igor"; break;
                case "007": saltzaileaIzena = "Mario"; break;
                default: saltzaileaIzena = "Autosalmenta"; break;
            }
            if (!int.TryParse(saltzaileaId, out int idZenbakia))
                idZenbakia = 0;

            
            Saltzailea s = new Saltzailea(idZenbakia, saltzaileaIzena);

            if (!int.TryParse(zatitu[2], out int PrezioaKg))
                PrezioaKg = 0;
            if (!decimal.TryParse(zatitu[3], out decimal Pisua))
                Pisua = 0;
            if (!decimal.TryParse(zatitu[4], out decimal Prezioa))
                Prezioa = 0;

            Produktua p = new Produktua(produktuaIzena, PrezioaKg, Pisua, Prezioa);

           
            Ticket t = new Ticket(nextId.ToString(), fechaTicket, s, Prezioa);
            nextId++; 

            
            t.GehituProduktua(produktuaIzena, PrezioaKg, Prezioa, Pisua);

            t.Baskula = baskula;

            ticketak.Add(t);
            
         
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            
            Console.WriteLine($"Tiketa sortua: {produktuaIzena} - {saltzaileaIzena} - {fechaTicket.ToShortDateString()} {fechaTicket.ToShortTimeString()}");
            Console.ForegroundColor = prevColor;
        }

        Console.WriteLine("Sortu diren tiket guztiak: " + ticketak.Count);
        return ticketak;
    }
}
