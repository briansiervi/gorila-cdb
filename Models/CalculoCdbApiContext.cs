using Microsoft.EntityFrameworkCore;

namespace gorila_cdb.Models
{
    public class gorila_cdbContext : DbContext
    {
        public gorila_cdbContext(DbContextOptions<gorila_cdbContext> options): base(options)
        {
        }

        public DbSet<CdbInput> CdbInput { get; set; }
        public DbSet<CdbOutput> CdbOutput { get; set; }
    }
}