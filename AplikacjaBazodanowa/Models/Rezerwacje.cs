using System.ComponentModel.DataAnnotations;

namespace AplikacjaBazodanowa.Models
{
    public class Rezerwacje
    {
        [Key]
        public int Id_Rezerwacji { get; set; }
        public int Id_Pasazera { get; set; }
        public int Id_Lotu { get; set; }
        [Required]
        public DateTime Data_Rezerwacji { get; set; }

        // Właściwości nawigacyjne
        public Pasazerowie Pasazer { get; set; }
        public Loty Lot { get; set; }
    }
}