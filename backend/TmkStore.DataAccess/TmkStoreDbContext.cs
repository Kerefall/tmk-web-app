using Microsoft.EntityFrameworkCore;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess
{
    public class TmkStoreDbContext : DbContext
    {
        public TmkStoreDbContext(DbContextOptions<TmkStoreDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<ProductEntity> Products { get; set; }
    }
}
