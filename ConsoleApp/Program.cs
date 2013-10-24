using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DomainModels;
using Company.ReadModels;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository<Articolo> repository = new ListListino();
            var aggregateRoot = new ListinoAggregateRoot(repository);
            var dataContext = new ListinoDataContext(repository);
            List(dataContext);
            aggregateRoot.CreaArticolo("1", "AAA", 50, true, false);
            aggregateRoot.CreaArticolo("2", "BBB", 20, true, false);
            aggregateRoot.CreaArticolo("3", "CCC", 10, true, false);
            aggregateRoot.CreaArticolo("4", "DDD", 50, true, false);
            List(dataContext);

            Console.ReadLine();
        }

        static void List(ListinoDataContext dc)
        {
            var page = dc.Page(0, 10);

            Console.WriteLine("ArticoloId;Descrizione");
            foreach (var item in page.Items)
            {
                Console.WriteLine("{0};{1}", item.ArticoloId, item.Descrizione);
            }
        }
    }
}
