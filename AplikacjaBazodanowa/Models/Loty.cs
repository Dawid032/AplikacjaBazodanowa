using System.ComponentModel.DataAnnotations;

namespace AplikacjaBazodanowa.Models
{
    public class Loty
    {
        [Key]
        public int Id_Lotu { get; set; }
        [Required]
        public string Miasto_Odlotu { get; set; }
        [Required]
        public string Miasto_Przylotu { get; set; }
        [Required]
        public DateTime Data_Odlotu { get; set; }
        [Required]
        public DateTime Data_Przylotu { get; set; }
        public int Id_Samolotu { get; set; }

        // Właściwości nawigacyjne
        public Samoloty Samolot { get; set; }
        public ICollection<Rezerwacje> Rezerwacje { get; set; }
        public ICollection<Zaloga> Zaloga { get; set; }
    }
}
