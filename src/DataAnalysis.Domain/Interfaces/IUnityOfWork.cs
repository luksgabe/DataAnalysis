namespace DataAnalysis.Domain.Interfaces
{
    public interface IUnityOfWork
    {
        ISalesmanRepository salesmanRepository { get; }
        ICustomerRepository customerRepository { get; }
        ISaleRepository saleRepository { get; }
    }
}
