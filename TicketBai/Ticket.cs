using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBai
{
    class Ticket
    {
        // ===== PROPIETATEAK =====
        public string Id { get; set; }               // Ticket ID edo kodea
        public DateTime Data { get; set; }           // Data eta ordua
        public Saltzailea Saltzailea { get; set; }   // Saltzailea
        public List<Produktua> Produktuak { get; set; }  // Produktuen zerrenda

        // ===== KONSTRUKTOREA =====
        public Ticket(string id, DateTime data, Saltzailea saltzailea)
        {
            Id = id;
            Data = data;
            Saltzailea = saltzailea;
            Produktuak = new List<Produktua>();
        }

        // ===== METODOAK =====

        // Produktuak gehitzeko
        public void GehituProduktua(string izena, decimal prezioa)
        {
            Produktuak.Add(new Produktua(izena, prezioa));
        }

        // Guztizkoa kalkulatu
        public decimal KalkulatuTotala()
        {
            decimal totala = 0;
            foreach (var p in Produktuak)
            {
                totala += p.Prezioa;
            }
            return totala;
        }
    }
    // ===== Produktua klasea =====
    class Produktua
    {
        public string Izena { get; set; }
        public decimal Prezioa { get; set; }

        public Produktua(string izena, decimal prezioa)
        {
            Izena = izena;
            Prezioa = prezioa;
        }
    }
}
