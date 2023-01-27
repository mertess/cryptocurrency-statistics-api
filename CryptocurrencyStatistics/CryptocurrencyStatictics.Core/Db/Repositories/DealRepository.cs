using CryptocurrencyStatictics.Core.Db.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyStatictics.Core.Db.Repositories
{
    public class DealRepository : BaseEntityRepository<Deal>
    {
        public DealRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
