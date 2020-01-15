using System.Collections.Generic;

namespace DataAnalysis.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public long SaleId { get; set; }
        public string SalesmanName { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
