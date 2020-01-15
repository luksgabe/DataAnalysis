using DataAnalysis.Domain.Interfaces;
using DataAnalysis.Infra.Data.Repositories;
using System;

namespace DataAnalysis.Infra.IoC
{
    public class DependencyInjection
    {
        public static ISalesmanRepository _salesmanRepository
        {
            get
            {
                return new SalesmanRepository();
            }
        }
    }
}
