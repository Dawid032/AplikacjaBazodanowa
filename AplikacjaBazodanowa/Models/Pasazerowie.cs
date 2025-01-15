using System.ComponentModel.DataAnnotations;

namespace AplikacjaBazodanowa.Models
{
    public class Pasazerowie
    {
        [Key]
        public int Id_Pasazera { get; set; }
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }

        // Właściwości nawigacyjne
        public ICollection<Rezerwacje> Rezerwacje { get; set; }
    }
}