using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Models;
using System.Collections.Generic;
using System.IO;

namespace DataAnalysis.Domain.Interfaces
{
    public interface ISalesmanService
    {
        void SaveSalesman(List<FileModel> listFile);
        IEnumerable<Salesman> GetAllSalesmens();
    }
}
