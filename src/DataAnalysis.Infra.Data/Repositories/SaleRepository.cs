using DataAnalysis.Domain.Entities;
using DataAnalysis.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAnalysis.Infra.Data.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public override async Task InsertMany(List<Sale> listEntities)
        {
            long id = 0;

            if (AplicationContext.SaleCollection != null && AplicationContext.SaleCollection.Any())
            {
  
                var lastSale = AplicationContext.SaleCollection.LastOrDefault();
                id = lastSale.Id;

                listEntities.ForEach(p => p.Id = id++);

                listEntities = await Task.Run(() =>
                {
                    return listEntities.Select(sale => new Sale
                    {
                        Id = sale.Id,
                        SaleId = sale.SaleId,
                        SalesmanName = sale.SalesmanName,
                        Items = sale.Items.Select(item => new Item
                        {
                            Id = item.Id,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            SaleId = sale.Id
                        }).ToList()

                    }).ToList();

                });

                AplicationContext.SaleCollection.ToList().AddRange(listEntities);
            }
            else
            {
                id = 1;
                listEntities.ForEach(p => p.Id = id++);

                listEntities = await Task.Run(() =>
                {
                    return listEntities.Select(sale => new Sale
                    {
                        Id = sale.Id,
                        SaleId = sale.SaleId,
                        SalesmanName = sale.SalesmanName,
                        Items = sale.Items.Select(item => new Item
                        {
                            Id = item.Id,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            SaleId = sale.Id
                        }).ToList()

                    }).ToList();
                });

                AplicationContext.SaleCollection = listEntities.ToList();
            }
        }

        public override IEnumerable<Sale> GetAll()
        {
            return AplicationContext.SaleCollection.ToList();
        }
    }
}
