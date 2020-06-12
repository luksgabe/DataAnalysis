using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Interfaces;
using DataAnalysis.Domain.Models;

namespace DataAnalysis.Domain.Services
{
    public class SaleService : ServiceBase<Sale>, ISaleService
    {

        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public IEnumerable<Sale> GetAllSales()
        {
            return _saleRepository.GetAll();
        }

        public void SaveSalesman(List<FileModel> listFile)
        {
            var list = new List<Sale>();

            listFile.ForEach(file =>
            {
                Sale sale = ConvertFileToEntity(file);
                list.Add(sale);
            });

            _saleRepository.InsertMany(list);
        }

        public long ReturnIdSaleMostValuable()
        {
            var list = GetAllSales();
            List<Item> listItens = list.SelectMany(l => l.Items).ToList();
            var value = listItens.Max(p => p.Price);
            var item = listItens.FirstOrDefault(f => f.Price == value);
            var sale = list.FirstOrDefault(p => p.Id == item.SaleId);

            return sale.Id;
        }

        public string WorstSeller()
        {
            var list = GetAllSales();
            List<Item> listItens = list.SelectMany(l => l.Items).ToList();
            var value = listItens.Min(p => p.Price);
            var item = listItens.FirstOrDefault(f => f.Price == value);
            var sale = list.FirstOrDefault(p => p.Id == item.SaleId);

            return sale.SalesmanName;
        }

        protected override Sale ConvertFileToEntity(FileModel model)
        {
            string content = convertFile(model.FileInfo);

            string[] colluns = content.Split('�');
            long saleId = 0;
            long.TryParse(colluns[1], out saleId);

            var sale = new Sale
            {
                SaleId = saleId,
                SalesmanName = colluns[3],
                Items = convertNestedItens(colluns[2])
            };

            return sale;
        }

        private List<Item> convertNestedItens(string nested)
        {
            nested = nested.Replace("[", "").Replace("]", "");
            var list = nested.Split(',');

            var listItem = new List<Item>();

            list.ToList().ForEach(item =>
            {
                var id = 0;
                var quantity = 0;
                decimal price = 0;
                var prop = item.Split('-');
                int.TryParse(prop[0], out id);
                int.TryParse(prop[1], out quantity);
                decimal.TryParse(prop[2], out price);

                var newItem = new Item()
                {
                    Id = id,
                    Quantity = quantity,
                    Price = price
                };
                
                listItem.Add(newItem);
            });

            return listItem;
        }

        private string convertFile(FileInfo file)
        {
            using var reader = new StreamReader(file.FullName);
            return reader.ReadLine();
        }

        
    }
}
