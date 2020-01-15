namespace DataAnalysis.Domain.Entities
{
    public class Salesman : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public decimal Salary { get; set; }
    }
}
