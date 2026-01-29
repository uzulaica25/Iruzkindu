# TICKFY / TicketBai

**Laburpena**  
TICKFY (TicketBai) da ticketak automatikoki kudeatzeko sistema bat.  
- Ticketak objektuetan gordetzen ditu  
- Saltzaileak aldatzeko aukera ematen du  
- XML fitxategiak sortzen ditu eta datu-basean gordetzen ditu  
- Estatistikak modu automatikoan kalkulatzen ditu  
- Backup eta segurtasun kopiak egiten ditu  

---

## **Fitxategi nagusiak**

| Fitxategia | Azalpena |
|------------|----------|
| `Database.cs` | Datu-basearekin konektatzeko eta backup egiteko funtzioak |
| `Backup.cs` | Backup automatikoko funtzioak |
| `EmailSender.cs` | Email bidezko notifikazioak bidaltzeko funtzioak |
| `Estatistika.cs` | Saltzaile eta produktu estatistikak kalkulatzeko funtzioak |
| `Programa.cs` | Main menu eta programaren logika nagusia |
| `Saltzailea.cs` | Saltzaile objektuak kudeatzeko klasea |
| `Ticket.cs` | Ticket objektua definitzen du |
| `TicketFactory.cs` | Ticket objektuak sortzeko factory klasea |
| `XmlGenerator.cs` | Ticket-etik XML sortzeko funtzioak |
| `TicketBai.csproj` | Visual Studio proiektua |

---

## **Funtzionalitate nagusiak**

1. Ticketak irakurri eta objektuetan gordetzea  
2. Saltzailearen aldaketa aukeratzeko aukera  
3. XML sortzea eta balidatzea  
4. Datu-basean gordetzea (MySQL)  
5. Backup automatikoa egitea  
6. Estatistikak:  
   - Saltzaile bakoitzaren salmenta handiena  
   - Saltzaile bakoitzak zenbat ticket saldu dituen  
   - Eguneko ticket kopurua  
   - Produktu bakoitza zenbat ticket desberdinetan agertzen den  

---

## **Instalazioa**

1. Clone egin proiektua:

```bash
(git clone https://github.com/uzulaica25/TicketBai.git)
