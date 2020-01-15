using System.IO;

namespace DataAnalysis.Domain.Interfaces
{
    public interface IFileValidation
    {
        void DirectoryValidate(DirectoryInfo directory);
        void FileValidate(FileInfo file);
    }
}
