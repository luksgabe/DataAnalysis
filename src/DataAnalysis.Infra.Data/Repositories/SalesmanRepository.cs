using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Infra.Data.Repositories
{
    public class SalesmanRepository : Repository<Salesman>, ISalesmanRepository
    {
        public override void InsertMany(List<Salesman> listEntities)
        {
            if (AplicationContext.SalesmanCollection != null && AplicationContext.SalesmanCollection.Any())            
                AplicationContext.SalesmanCollection.ToList().AddRange(listEntities);
            else
                AplicationContext.SalesmanCollection = listEntities.ToList();
        }

        public override IEnumerable<Salesman> GetAll()
        {
            return AplicationContext.SalesmanCollection.ToList();
        }
    }
}
