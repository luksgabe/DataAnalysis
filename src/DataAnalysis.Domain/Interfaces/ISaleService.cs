using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Models;
using System.Collections.Generic;

namespace DataAnalysis.Domain.Interfaces
{
    public interface ISaleService
    {
        void SaveSalesman(List<FileModel> listFile);
        IEnumerable<Sale> GetAllSales();
        long ReturnIdSaleMostValuable();
        string WorstSeller();
    }
}
