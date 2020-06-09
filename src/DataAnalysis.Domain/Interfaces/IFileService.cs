using DataAnalysis.Domain.Enumerators;
using DataAnalysis.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DataAnalysis.Domain.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileModel>> GetByType(List<FileInfo> listFile, TypeData type);
        Task CreateReport();
    }
}
