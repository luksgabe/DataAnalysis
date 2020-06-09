using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAnalysis.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task InsertMany(List<TEntity> listEntities);
    }
}
