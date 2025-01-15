using System.ComponentModel.DataAnnotations;

namespace AplikacjaBazodanowa.Models
{
    public class Pracownicy
    {
        [Key]
        public int Id_Pracownika { get; set; }
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        public string Stanowisko { get; set; }
        [Required]
        public decimal Pensja { get; set; }
    }
}