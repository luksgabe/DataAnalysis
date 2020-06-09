using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAnalysis.Infra.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public override async Task InsertMany(List<Customer> listEntities)
        {
            await Task.Run(() =>
            {
                if (AplicationContext.CustomerCollection != null && AplicationContext.CustomerCollection.Any())
                    AplicationContext.CustomerCollection.ToList().AddRange(listEntities);
                else
                    AplicationContext.CustomerCollection = listEntities.ToList();
            });          
        }

        public override IEnumerable<Customer> GetAll()
        {
            return AplicationContext.CustomerCollection.ToList();
        }
    }
}
