using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAnalysis.Domain.Interfaces
{
    public interface ICustomerService 
    {
        Task SaveCustomer(List<FileModel> listFile);
        IEnumerable<Customer> GetAllCustomers();
    }
}
