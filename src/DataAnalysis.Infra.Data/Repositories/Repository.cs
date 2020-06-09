
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAnalysis.Domain.Interfaces;

namespace DataAnalysis.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
 
        public virtual IEnumerable<TEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task InsertMany(List<TEntity> entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
