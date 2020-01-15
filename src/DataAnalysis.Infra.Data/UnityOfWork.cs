using DataAnalysis.Domain.Interfaces;
using DataAnalysis.Infra.Data.Repositories;

namespace DataAnalysis.Infra.Data
{
    public class UnityOfWork : IUnityOfWork
    {
        private ISalesmanRepository _salesmanRepository;
        private ICustomerRepository _customerRepository;
        private ISaleRepository _saleRepository;

        public ISalesmanRepository salesmanRepository
        {
            get
            {
                return _salesmanRepository = _salesmanRepository ?? new SalesmanRepository();
            }
        }

        public ICustomerRepository customerRepository
        {
            get
            {
                return _customerRepository = _customerRepository ?? new CustomerRepository();
            }
        }

        public ISaleRepository saleRepository
        {
            get
            {
                return _saleRepository = _saleRepository ?? new SaleRepository();
            }
        }

    }
}
