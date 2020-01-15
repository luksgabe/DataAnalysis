using DataAnalysis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis.Infra.Data
{
    public class AplicationContext
    {
        public static ICollection<Salesman> SalesmanCollection { get; set; }
        public static ICollection<Customer> CustomerCollection { get; set; }
        public static ICollection<Sale> SaleCollection { get; set; }

    }
}
