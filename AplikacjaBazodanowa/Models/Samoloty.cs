using System.ComponentModel.DataAnnotations;

namespace AplikacjaBazodanowa.Models
{
    public class Samoloty
    {
        [Key]
        public int Id_Samolotu { get; set; }  // Klucz główny

        [Required]
        public string Model { get; set; }  // Model samolotu

        [Required]
        public int Liczba_Miejsc { get; set; }  // Liczba miejsc w samolocie

        public int Id_Linii { get; set; }  // Id linii lotniczej

        // Właściwości nawigacyjne
        public LinieLotnicze LinieLotnicze { get; set; }  // Na jakie linie lotnicze jest przypisany samolot
        public ICollection<Loty> Loty { get; set; }  // Kolekcja lotów przypisanych do samolotu
    }
}
