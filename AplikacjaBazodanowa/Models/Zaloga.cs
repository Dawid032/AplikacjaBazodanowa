using System.ComponentModel.DataAnnotations;

namespace AplikacjaBazodanowa.Models
{
    public class Zaloga
    {
        [Key]
        public int Id { get; set; }
        public int Id_Lotu { get; set; }
        public int Id_Pilota { get; set; }
        [Required]
        public string Funkcja { get; set; }

        // Właściwości nawigacyjne
        public Loty Lot { get; set; }
        public Piloci Pilot { get; set; }
    }
}