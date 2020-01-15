namespace DataAnalysis.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string BusinesArea { get; set; }
    }
}
