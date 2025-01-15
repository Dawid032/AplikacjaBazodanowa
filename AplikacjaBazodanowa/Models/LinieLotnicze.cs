using System.ComponentModel.DataAnnotations;

namespace AplikacjaBazodanowa.Models
{
    public class LinieLotnicze
    {
        [Key]
        public int Id_Linii { get; set; }
        [Required]
        public string Nazwa { get; set; }
        [Required]
        public string Kraj_Pochodzenia { get; set; }

        // Właściwości nawigacyjne
        public ICollection<Samoloty> Samoloty { get; set; }
        public ICollection<Piloci> Piloci { get; set; }
    }
}