using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThreeTierApp.Models;

namespace ThreeTierApp.Controllers
{
    public class ListinoController : Controller
    {
        private Listino Listino
        {
            get
            {
                if (Session["listino"] == null)
                {
                    Session["listino"] = new Listino();
                }
                return (Listino)Session["listino"];
            }
        }


        //
        // GET: /Listino/

        public ActionResult Index()
        {
            var items =
                from item in Listino
                select new ArticoloDTO { 
                    ArticoloId = item.ArticoloId
                    , Codice = item.Codice 
                };

            return View(items);
        }

        // GET: /Listino/Details/5

        public ActionResult Details(int id)
        {
            var item = Listino.Single(xx => xx.ArticoloId == id);
            return View(item);
        }

        //
        // GET: /Listino/Create

        public ActionResult Create()
        {
            var item = new Articolo();
            return View("Edit", item);
        } 

        //
        // POST: /Listino/Create
        private bool ArticoloMaggioreDiZero(Articolo a)
        {
            return a.ArticoloId > 0;
        }

        [HttpPost]
        public ActionResult Create(Articolo articolo)
        {
            try
            {
                var existing = new Articolo();
                existing.Codice = articolo.Codice;
                existing.Descrizione = articolo.Descrizione;
                existing.CategoriaId = articolo.CategoriaId;

                // TODO: Add insert logic here
                existing.ArticoloId =
                    Listino
                    .Where(ArticoloMaggioreDiZero)
                    .DefaultIfEmpty(new Articolo
                    {
                        ArticoloId = 0
                    })
                    .Select(xx => xx.ArticoloId)
                    .Max() + 1;

                if (Listino.Any())
                {
                    existing.ArticoloId = Listino.Max(xx => xx.ArticoloId) + 1;
                }
                else
                {
                    existing.ArticoloId = 1;
                }

                Listino.Add(existing);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Listino/Edit/5
 
        public ActionResult Edit(int? id, int? categoryId)
        {
            Articolo item = null;
            if (id.HasValue)
            {
                item = Listino.Single(xx => xx.ArticoloId == id);
            }
            else
            {
                item = new Articolo();
                if (categoryId.HasValue)
                    item.CategoriaId = categoryId.Value;
            }
            ViewBag.hasCategoryIdFixed = categoryId.HasValue;
            return View(item);
        }

        //
        // POST: /Listino/Edit/5

        [HttpPost]
        public ActionResult Edit(int? id, Articolo articolo)
        {
            try
            {
                Articolo existing = null;
                if (articolo.ArticoloId > 0)
                {
                    existing = Listino.Single(xx => xx.ArticoloId == articolo.ArticoloId);
                }
                else
                {
                    existing = new Articolo();
                    existing.ArticoloId =
                        Listino
                        .Where(ArticoloMaggioreDiZero)
                        .DefaultIfEmpty(new Articolo
                        {
                            ArticoloId = 0
                        })
                        .Select(xx => xx.ArticoloId)
                        .Max() + 1;

                    Listino.Add(existing);
                }

                existing.Codice = articolo.Codice;
                existing.Descrizione = articolo.Descrizione;
                existing.CategoriaId = articolo.CategoriaId;
                existing.Prezzo = articolo.Prezzo;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Listino/Delete/5
 
        public ActionResult Delete(int id)
        {
            var item = Listino.Single(xx => xx.ArticoloId == id);
            Listino.Remove(item);
            return View();
        }

        //
        // POST: /Listino/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
