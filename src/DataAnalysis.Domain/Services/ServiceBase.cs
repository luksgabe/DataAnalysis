using DataAnalysis.Domain.Models;
using System.IO;

namespace DataAnalysis.Domain.Services
{
    public abstract class ServiceBase<TEntity> where TEntity : class
    {
        protected abstract TEntity ConvertFileToEntity(FileModel fileInfo);
        
    }
}
