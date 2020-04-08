using Microsoft.EntityFrameworkCore;

namespace gorila_cdb.Models
{
    public class cdbContext : DbContext
    {
        public cdbContext(DbContextOptions<cdbContext> options): base(options)
        {
        }

        public DbSet<CdbInput> CdbInput { get; set; }
        public DbSet<CdbOutput> CdbOutput { get; set; }
    }
}