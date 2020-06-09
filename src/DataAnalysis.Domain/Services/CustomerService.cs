using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Interfaces;
using DataAnalysis.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataAnalysis.Domain.Services
{
    public class CustomerService : ServiceBase<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll().ToList();
        }

        public async Task SaveCustomer(List<FileModel> listFile)
        {
            var list = new List<Customer>();

            listFile.ForEach(file =>
            {
                Customer customer = ConvertFileToEntity(file);
                list.Add(customer);
            });

            await _customerRepository.InsertMany(list);
        }

        protected override Customer ConvertFileToEntity(FileModel model)
        {
            string content = convertFile(model.FileInfo);

            string[] colluns = content.Split('�');

            var customer = new Customer
            {
                Name = colluns[2],
                Cnpj = colluns[1],
                BusinesArea = colluns[3]
            };

            return customer;
        }

        private string convertFile(FileInfo file)
        {
            using var reader = new StreamReader(file.FullName);
            return reader.ReadLine();
        }
    }
}
