using DataAnalysis.Application;
using System;
using System.Threading.Tasks;

namespace DataAnalysis
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var flow = new Flow();
            await flow.Start();
        }
    }
}
