using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Company.DomainModels;
using Newtonsoft.Json;

namespace ThreeTierApp.Controllers
{
    public class RepositoryArticoloController : Controller, IRepository<Articolo>
    {
        private List<Articolo> _listino;
        private List<Articolo> Listino
        {
            get
            {
                if (_listino == null)
                {
                    var listino_json = "[]";
                    if (Request.Cookies["listino"] != null) 
                            listino_json = Request.Cookies["listino"].Value;
                    _listino = JsonConvert.DeserializeObject<List<Articolo>>(listino_json);
                }
                return _listino;
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (_listino != null)
            {
                Response.Cookies["listino"].Value = JsonConvert.SerializeObject(_listino);
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
