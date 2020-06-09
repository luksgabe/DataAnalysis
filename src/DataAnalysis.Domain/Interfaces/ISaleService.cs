using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAnalysis.Domain.Interfaces
{
    public interface ISaleService
    {
        Task SaveSalesman(List<FileModel> listFile);
        IEnumerable<Sale> GetAllSales();
        long ReturnIdSaleMostValuable();
        string WorstSeller();
    }
}
