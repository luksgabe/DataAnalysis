using DataAnalysis.Domain.Enumerators;
using DataAnalysis.Domain.Interfaces;
using DataAnalysis.Domain.Models;
using DataAnalysis.Domain.Services;
using DataAnalysis.Domain.Validations;
using DataAnalysis.Infra.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataAnalysis.Application
{
    public class Flow
    {
        private readonly IFileService _fileService;
        private readonly ISalesmanService _salesmanService;
        private readonly ICustomerService _customerService;
        private readonly ISaleService _saleService;
        private readonly IFileValidation _fileValidation;
        private readonly IUnityOfWork _unityOfWork;
        private static List<FileInfo> listaArquivos { get; set; }

        public Flow()
        {
            _unityOfWork = new UnityOfWork();
            _fileValidation = new FileValidation();
            _fileService = new FileService(_unityOfWork);
            _salesmanService = new SalesmanService(_unityOfWork.salesmanRepository);
            _customerService = new CustomerService(_unityOfWork.customerRepository);
            _saleService = new SaleService(_unityOfWork.saleRepository);
        }

        public async Task Start()
        {
            try
            {
                await ReadSalesmanData();
                await ReadCustomerData();
                await ReadSalesData();
                await CreateReport();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CreateReport()
        {
            await _fileService.CreateReport();
        }

        private async Task ReadSalesmanData()
        {
            DirectoryInfo diretorio = await returnsDirectory();
            List<FileModel> list = (await BrowseFiles(diretorio, TypeData.SalesMan)).ToList();
            await _salesmanService.SaveSalesman(list);
        }

        private async Task ReadCustomerData()
        {
            DirectoryInfo diretorio = await returnsDirectory();
            List<FileModel> list = (await BrowseFiles(diretorio, TypeData.Customer)).ToList();
            await _customerService.SaveCustomer(list);
        }

        private async Task ReadSalesData()
        {
            DirectoryInfo diretorio = await returnsDirectory();
            List<FileModel> list = (await BrowseFiles(diretorio, TypeData.Sale)).ToList();
            await _saleService.SaveSalesman(list);
        }

        private async Task<IEnumerable<FileModel>> BrowseFiles(DirectoryInfo dir, TypeData typeID)
        {
            if (listaArquivos != null && listaArquivos.Any())
                return await _fileService.GetByType(listaArquivos, typeID);
            else
            {
                _fileValidation.DirectoryValidate(dir);


                //alterar aqui para ficar observando pasta de diretório
                listaArquivos = new List<FileInfo>();
                dir.GetFiles().ToList().ForEach(file =>
                {
                    _fileValidation.FileValidate(file);
                    listaArquivos.Add(file);
                });

                return await _fileService.GetByType(listaArquivos.ToList(), typeID);
            }
        }

        private async Task<DirectoryInfo> returnsDirectory()
        {
            var homeDrive = Environment.GetEnvironmentVariable("HOMEDRIVE");
            var homePath = Environment.GetEnvironmentVariable("HOMEPATH");
            var directory = string.Empty;

            if (!string.IsNullOrWhiteSpace(homeDrive) && !string.IsNullOrWhiteSpace(homePath))
            {
                directory = await Task.Run(() => {
                    var fullHomePath = homeDrive + Path.DirectorySeparatorChar + homePath;
                    return Path.Combine(fullHomePath, "data\\in");
                });
            }
            else
            {
                throw new Exception("Erro de variável no sistema operaciona, não existe variável HOMEPATH OU HOMEDRIVE");
            }

            //string pastaUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //var nomeDiretorio = string.Format("{0}\\data\\in", pastaUser);
            return new DirectoryInfo(directory);
        }
    }
}
