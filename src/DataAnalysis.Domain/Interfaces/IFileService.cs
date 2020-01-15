using DataAnalysis.Domain.Enumerators;
using DataAnalysis.Domain.Models;
using System.Collections.Generic;
using System.IO;

namespace DataAnalysis.Domain.Interfaces
{
    public interface IFileService
    {
        IEnumerable<FileModel> GetByType(List<FileInfo> listFile, TypeData type);
        void CreateReport();
    }
}
