using System;
using System.IO;
using DataAnalysis.Domain.Interfaces;

namespace DataAnalysis.Domain.Validations
{
    public class FileValidation : IFileValidation
    {
        public void DirectoryValidate(DirectoryInfo directory)
        {
            DirectoryExists(directory);
            FilesExists(directory);
        }

        public void FileValidate(FileInfo file)
        {
            ValidateFileExtension(file.Extension);
            ValidateFileContent(file);
        }
        private void ValidateFileExtension(string extension)
        {
            if (extension != ".dat" )
                throw new Exception("Arquivo em formato inválido.");
        }

        private void ValidateFileContent(FileInfo file)
        {
            var conteudo = string.Empty;

            using var reader = new StreamReader(file.FullName);
            conteudo = reader.ReadLine();

            if (conteudo.Length <= 0)
                throw new Exception("Arquivo vazio.");

            if (!conteudo.Contains('�'))
                throw new Exception(string.Format(@"Não foi possivel normalizar dados em arquivo {0},
                    verifique o mesmo e garanta que dados sejam separados por 'ç'", file.Name));
        }

        private void DirectoryExists(DirectoryInfo directory)
        {
            if (!directory.Exists)
            {
                string nomeDiretorio = directory.FullName;
                throw new Exception(string.Format("Diretório {0} não existe.", nomeDiretorio));
            }
        }

        private void FilesExists(DirectoryInfo directory)
        {
            int numeroArquivos = directory.GetFiles().Length;
            if (numeroArquivos <= 0)
            {
                throw new Exception("Não há arquivos no diretório.");
            }
        }
    }
}
