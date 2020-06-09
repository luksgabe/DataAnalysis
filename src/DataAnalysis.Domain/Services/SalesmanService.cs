using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Interfaces;
using DataAnalysis.Domain.Models;

namespace DataAnalysis.Domain.Services
{
    public class SalesmanService : ServiceBase<Salesman>, ISalesmanService
    {
        private readonly ISalesmanRepository _salesmanRepository;

        public SalesmanService(ISalesmanRepository salesmanRepository)
        {
            _salesmanRepository = salesmanRepository;
        }

        public IEnumerable<Salesman> GetAllSalesmens()
        {
            return _salesmanRepository.GetAll().ToList();
        }

        public async Task SaveSalesman(List<FileModel> listFile)
        {
            var list = new List<Salesman>();

            listFile.ForEach(file =>
            {
                Salesman salesman = ConvertFileToEntity(file);
                list.Add(salesman);
            });

            await _salesmanRepository.InsertMany(list);
        }



        protected override Salesman ConvertFileToEntity(FileModel model)
        {
            string content = convertFile(model.FileInfo);

            string[] colluns = content.Split('�');
            decimal salary = 0;
            decimal.TryParse(colluns[3], out salary);

            var salesman = new Salesman
            {
                Name = colluns[2],
                Cpf = colluns[1],
                Salary = salary
            };

            return salesman;
        }

        private string convertFile(FileInfo file)
        {
            using var reader = new StreamReader(file.FullName);
            return reader.ReadLine();
        }
    }
}
