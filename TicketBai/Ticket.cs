using Mysqlx.Crud;
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
        public string Baskula { get; set; }        // Baskula zenbakia
        public string Id { get; set; }               // Ticket ID edo kodea
        public string Eguna { get; set; }    // Adib: "2023-10-25"
        public string Ordua { get; set; }    // Adib: "15:30:00"         // Data eta ordua
        public Saltzailea Saltzailea { get; set; }   // Saltzailea
        public List<Produktua> Produktuak { get; set; }  // Produktuen zerrenda

        // ===== KONSTRUKTOREA =====
        public Ticket(string id, DateTime dataOsoa, Saltzailea saltzailea)
        {
            Id = id;
     
            Eguna = dataOsoa.ToString("yyyy-MM-dd"); // Urtea-Hilabetea-Eguna
            Ordua = dataOsoa.ToString("HH:mm:ss");
            Saltzailea = saltzailea;
            Produktuak = new List<Produktua>();
        }

        // ===== METODOAK =====

        // Produktuak gehitzeko
        public void GehituProduktua(string izena,decimal prezioaKg, decimal prezioa, decimal pisua )
        {
            Produktuak.Add(new Produktua(izena,prezioaKg, prezioa, pisua));
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
        public decimal PrezioaKg { get; set; }
        public decimal Prezioa { get; set; }
        public decimal Pisua { get; set; }

        public Produktua(string izena,decimal prezioaKg, decimal prezioa, decimal pisua)
        {
            
            Izena = izena;
            PrezioaKg = prezioaKg;
            Prezioa = prezioa;
            Pisua = pisua;
        }
    }
}
