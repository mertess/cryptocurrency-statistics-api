using System;
using System.ComponentModel.DataAnnotations;

namespace CryptocurrencyStatictics.Core.Db.DbModels
{
    public class Deal : BaseEntity
    {
        [Required]
        public string Currencies { get; set; }
        public decimal LastCost { get; set; }
        public DateTimeOffset UpdatedAtUtc { get; set; }
    }
}
