using Microsoft.EntityFrameworkCore;
using ProiectBigData.Models;

namespace ProiectBigData.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Spital> Spitale { get; set; }
        public DbSet<Specializare> Specializari { get; set; }
        public DbSet<Doctor> Doctori { get; set; }

        public DbSet<Programare> Programari { get; set; }

        public DbSet<StireMedicala> StiriMedicale { get; set; }
    }
}
