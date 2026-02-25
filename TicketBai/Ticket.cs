using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBai
{

    /// <summary>
    /// Ticket klasea, saltzailearen informazioa, data eta ordua, produktuen zerrenda eta prezio osoa gordetzeko erabiltzen da. Konstruktorea ticket ID-a, data osoa (DateTime objektua), saltzailea eta prezio osoa (aukerakoa) jasotzen du. GehituProduktua metodoak produktuak ticket-era gehitzeko aukera ematen du. Prezio osoa ere gordetzen da, eta behar izanez gero kalkulatu daiteke produktuen prezioen batura gisa.
    /// </summary>
    class Ticket
    {
        // ===== PROPIETATEAK =====
        public string Baskula { get; set; }        // Baskula zenbakia
        public string Id { get; set; }               // Ticket ID edo kodea
        public string Eguna { get; set; }    // Adib: "2023-10-25"
        public string Ordua { get; set; }    // Adib: "15:30:00"         // Data eta ordua
        public Saltzailea Saltzailea { get; set; }   // Saltzailea
        public List<Produktua> Produktuak { get; set; }  // Produktuen zerrenda
        public decimal PrezioOsoa { get; set; } // ← Hemen gehitu


        // ===== KONSTRUKTOREA =====

        /// <summary>
        /// Initializes a new instance of the <see cref="Ticket"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataOsoa">The data osoa.</param>
        /// <param name="saltzailea">The saltzailea.</param>
        /// <param name="prezioOsoa">The prezio osoa.</param>
        public Ticket(string id, DateTime dataOsoa, Saltzailea saltzailea, decimal prezioOsoa = 0)
        {
            Id = id;
     
            Eguna = dataOsoa.ToString("yyyy-MM-dd"); // Urtea-Hilabetea-Eguna
            Ordua = dataOsoa.ToString("HH:mm:ss");
            Saltzailea = saltzailea;
            Produktuak = new List<Produktua>();
            PrezioOsoa = prezioOsoa;
        }

        /// <summary>
        /// Produktua gehitzeko metodoa. Produktua izena, prezioa kg, prezioa eta pisua jasotzen ditu, eta produktua ticket-era gehitzen du. Produktuak zerrenda batean gordetzen dira, eta prezio osoa ere kalkulatzen da produktuen prezioen batura gisa. GehituProduktua metodoak produktuak ticket-era gehitzeko aukera ematen du, eta prezio osoa ere gordetzen da, behar izanez gero kalkulatu daiteke produktuen prezioen batura gisa.
        /// </summary>
        /// <param name="izena">The izena.</param>
        /// <param name="prezioaKg">The prezioa kg.</param>
        /// <param name="prezioa">The prezioa.</param>
        /// <param name="pisua">The pisua.</param>
        public void GehituProduktua(string izena,decimal prezioaKg, decimal prezioa, decimal pisua )
        {
            Produktuak.Add(new Produktua(izena,prezioaKg, prezioa, pisua));
        }

       
        
    }
    /// <summary>
    /// Produktua klasea, produktu bakoitzaren izena, prezioa kg, prezioa eta pisua gordetzeko erabiltzen da. Konstruktorea izena, prezioa kg, prezioa eta pisua jasotzen du. Produktuak ticket-era gehitzeko erabiliko da, eta prezio osoa kalkulatzeko erabiliko da produktuen prezioen batura gisa.
    /// </summary>
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
