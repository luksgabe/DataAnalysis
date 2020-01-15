namespace DataAnalysis.Domain.Entities
{
    public class Item : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public long SaleId { get; set; }
    }
}
