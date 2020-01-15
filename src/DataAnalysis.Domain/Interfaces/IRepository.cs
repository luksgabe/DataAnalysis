using System.Collections.Generic;

namespace DataAnalysis.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        void InsertMany(List<TEntity> listEntities);
    }
}
