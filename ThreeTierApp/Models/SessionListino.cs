using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Company.DomainModels;

namespace ThreeTierApp.Models
{
    public class SessionListino: IRepository<Articolo>
    {
        private List<Articolo> Listino
        {
            get
            {
                if (HttpContext.Current.Session["listino"] == null)
                {
                    HttpContext.Current.Session["listino"] = new List<Articolo>();
                }
                return (List<Articolo>)HttpContext.Current.Session["listino"];
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
