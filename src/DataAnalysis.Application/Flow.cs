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

        public void Start()
        {
            try
            {
                LerDadosVendedor();
                LerDadosCliente();
                LerDadosVendas();
                MontarRelatorio();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void MontarRelatorio()
        {
            _fileService.CreateReport();
        }

        private void LerDadosVendedor()
        {
            DirectoryInfo diretorio = retornaDiretorio();
            List<FileModel> list = BuscarArquivos(diretorio, TypeData.SalesMan).ToList();
            _salesmanService.SaveSalesman(list);
        }

        private void LerDadosCliente()
        {
            DirectoryInfo diretorio = retornaDiretorio();
            List<FileModel> list = BuscarArquivos(diretorio, TypeData.Customer).ToList();
            _customerService.SaveCustomer(list);
        }

        private void LerDadosVendas()
        {
            DirectoryInfo diretorio = retornaDiretorio();
            List<FileModel> list = BuscarArquivos(diretorio, TypeData.Sale).ToList();
            _saleService.SaveSalesman(list);
        }

        private IEnumerable<FileModel> BuscarArquivos(DirectoryInfo dir, TypeData typeID)
        {
            if (listaArquivos != null && listaArquivos.Any())
            {
                return _fileService.GetByType(listaArquivos, typeID);
            }
            else
            {
                _fileValidation.DirectoryValidate(dir);

                listaArquivos = new List<FileInfo>();
                dir.GetFiles().ToList().ForEach(file =>
                {
                    _fileValidation.FileValidate(file);
                    listaArquivos.Add(file);
                });

                return  _fileService.GetByType(listaArquivos.ToList(), typeID);
            }
        }

        private DirectoryInfo retornaDiretorio()
        {
            string pastaUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var nomeDiretorio = string.Format("{0}\\data\\in", pastaUser);
            return new DirectoryInfo(nomeDiretorio);
        }
    }
}
