using CryptocurrencyStatictics.Core.Db.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CryptocurrencyStatictics.Core.Db
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Deal> Deals { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
