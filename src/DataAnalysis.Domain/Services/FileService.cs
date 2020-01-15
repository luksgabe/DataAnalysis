using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Enumerators;
using DataAnalysis.Domain.Interfaces;
using DataAnalysis.Domain.Models;

namespace DataAnalysis.Domain.Services
{
    public class FileService : IFileService
    {
        private readonly ICustomerService _customerService;
        private readonly ISalesmanService _salesmanService;
        private readonly ISaleService _saleService;

        public FileService(IUnityOfWork unityOfWork)
        {
            _customerService = new CustomerService(unityOfWork.customerRepository);
            _salesmanService = new SalesmanService(unityOfWork.salesmanRepository);
            _saleService = new SaleService(unityOfWork.saleRepository);
        }

        public void CreateReport()
        {
            DirectoryInfo diretorio = retornaDiretorioSaida();
            using var writer = new StreamWriter(string.Format("{0}\\{{flat_file_name}}.done.dat", diretorio.FullName));
            string conteudo = escreverArquivo();
            writer.Write(conteudo);
        }

        private string escreverArquivo()
        {
            IEnumerable<Salesman> listSalesmans = _salesmanService.GetAllSalesmens().ToList();
            IEnumerable<Customer> listCustomers = _customerService.GetAllCustomers().ToList();
            IEnumerable<Sale> listSales = _saleService.GetAllSales().ToList();

            string conteudo = @$"
                Numero de Clientes: {listCustomers.Count()}
                Numero de Vendedores: {listSalesmans.Count()}
                Id da venda mais cara: { _saleService.ReturnIdSaleMostValuable() }
                Pior vendedor: { _saleService.WorstSeller() }
            ";

            return conteudo;
        }

        public IEnumerable<FileModel> GetByType(List<FileInfo> listFile, TypeData type)
        {
            List<FileModel> listModel = listFile.Where(w => tipoSelecionado(w, type)).Select(p => new FileModel {
                FileInfo = p,
                TypeData = type
            }).ToList();
            return listModel;
        }

        private bool tipoSelecionado(FileInfo file, TypeData type)
        {
            using var reader = new StreamReader(file.FullName);
            string conteudo = reader.ReadLine();
            return conteudo.Contains(retornaTipoDado(type));
        }

        private string retornaTipoDado(TypeData type)
        {
            var retorno = string.Empty;
            switch (type)
            {
                case TypeData.SalesMan:
                    retorno = "001";
                    break;
                case TypeData.Customer:
                    retorno = "002";
                    break;
                case TypeData.Sale:
                    retorno = "003";
                    break;
                default:
                    break;
            }
            return retorno;
        }

        private DirectoryInfo retornaDiretorioSaida()
        {
            string pastaUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var nomeDiretorio = string.Format("{0}\\data\\out", pastaUser);
            return new DirectoryInfo(nomeDiretorio);
        }
    }
}
