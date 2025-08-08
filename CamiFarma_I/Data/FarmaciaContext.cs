using Microsoft.EntityFrameworkCore;
using CamiFarma_I.Models; 

namespace CamiFarma_I.Data
{
    public class FarmaciaContext : DbContext
    {
        public FarmaciaContext(DbContextOptions<FarmaciaContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }

    }
}
