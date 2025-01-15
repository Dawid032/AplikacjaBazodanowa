using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AplikacjaBazodanowa.Models;

namespace AplikacjaBazodanowa.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Zmiana: Usunięcie DbSet<Lot> oraz nieistniejącej klasy Lot
        public DbSet<Loty> Loty { get; set; }
        public DbSet<Samoloty> Samoloty { get; set; }
        public DbSet<LinieLotnicze> LinieLotnicze { get; set; }
        public DbSet<Rezerwacje> Rezerwacje { get; set; }
        public DbSet<Pasazerowie> Pasazerowie { get; set; }
        public DbSet<Piloci> Piloci { get; set; }
        public DbSet<Zaloga>? Zaloga { get; set; }
        public DbSet<Pracownicy>? Pracownicy { get; set; }

        // Zdefiniowanie klucza głównego w metodzie OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Usunięcie konfiguracji dla klasy Lot, bo jej już nie ma

            // Definicja klucza głównego w encji 'Samoloty'
            modelBuilder.Entity<Samoloty>()
                .HasKey(s => s.Id_Samolotu);  // Zakładając, że 'Id_Samolotu' to klucz główny

            // Możesz dodać inne konfiguracje dla pozostałych encji, jeśli chcesz
        }
    }
}
