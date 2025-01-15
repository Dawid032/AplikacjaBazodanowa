
using System.ComponentModel.DataAnnotations;

namespace AplikacjaBazodanowa.Models
{
    public class Piloci
    {
        [Key]
        public int Id_Pilota { get; set; }
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        public int Id_Linii { get; set; }

        // Właściwości nawigacyjne
        public LinieLotnicze LinieLotnicze { get; set; }
        public ICollection<Zaloga> Zaloga { get; set; }
    }
}