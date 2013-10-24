using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DomainModels;

namespace ConsoleApp
{
    public class ListListino: IRepository<Articolo>
    {
        private List<Articolo> _listino = new List<Articolo>();

        private List<Articolo> Listino
        {
            get
            {
                return _listino;
            }
        }

        public void Add(Articolo item)
        {
            item.ArticoloId =
                Listino
                // .Where(ArticoloMaggioreDiZero)
                .DefaultIfEmpty(new Articolo
                {
                    ArticoloId = 0
                })
                .Select(xx => xx.ArticoloId)
                .Max() + 1;

            Listino.Add(item);
        }

        public void Update(Articolo item)
        {
            var existing = Listino.SingleOrDefault(xx => xx.ArticoloId == item.ArticoloId);
            if (existing != null) Listino.Remove(item);
            Listino.Add(item);
        }

        public IQueryable<Articolo> Items
        {
            get { return Listino.AsQueryable(); }
        }
    }
}
