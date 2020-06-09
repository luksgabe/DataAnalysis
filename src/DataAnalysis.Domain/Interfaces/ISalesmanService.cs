using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DataAnalysis.Domain.Interfaces
{
    public interface ISalesmanService
    {
        Task SaveSalesman(List<FileModel> listFile);
        IEnumerable<Salesman> GetAllSalesmens();
    }
}
