using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Models;
using System.Collections.Generic;

namespace DataAnalysis.Domain.Interfaces
{
    public interface ICustomerService 
    {
        void SaveCustomer(List<FileModel> listFile);
        IEnumerable<Customer> GetAllCustomers();
    }
}
