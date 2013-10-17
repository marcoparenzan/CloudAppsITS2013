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

        public ActionResult Index(int pageNumber = 0, int pageSize = 10)
        {
            var query = Listino.AsQueryable();
            //query = query.Where(xx => xx.ArticoloId > 0);
            //query = query.Where(xx => xx.Codice == "A");
            var query2 = query.Select(xx => new ArticoloDTO
            {
                ArticoloId = xx.ArticoloId
                ,
                Descrizione = xx.Descrizione
            });
            query2 = query2.OrderBy(xx => xx.Descrizione);

            //ViewBag.pageCount = query2.Count();
            //var items = 
            //var page = new PageOf<ArticoloDTO>(
            //    query2
            //    , pageNumber
            //    , pageSize
            //);
            var page = query2.Page(pageNumber, pageSize);
            //page = PageOfExtension.Page(query2, pageNumber, pageSize);

            //var xxx = 
            //    Listino
            //    .AsQueryable()
            //    .Where(xx => xx.Descrizione.StartsWith("A"))
            //    .Select(xx => new { ArticoloId = xx.ArticoloId})
            //    .OrderBy(xx => xx.ArticoloId)
            //    .Skip(10)
            //    .Take(10)
            //    .ToList();

            return View(page);
            // return View(items);
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
            var item = new ArticoloViewModel();
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
            }
                if (categoryId.HasValue)
                    item.CategoriaId = categoryId.Value;
            // ViewBag.hasCategoryIdFixed = categoryId.HasValue;

            var promozioni = item.Promozioni == null ? new PromozioniArticolo() : item.Promozioni;

            var viewModel = new ArticoloViewModel
            {
                Codice = item.Codice
                ,
                Descrizione = item.Descrizione
                ,
                Prezzo = item.Prezzo
                ,
                Categoria = item.CategoriaId.ToString()
                ,
                CategoryFixed = categoryId.HasValue
                ,
                PromozioneInOfferta = promozioni.InOfferta
                ,
                PromozioneMetaPrezzo = promozioni.MetaPrezzo
            };

            return View(viewModel);
        }

        //
        // POST: /Listino/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, ArticoloViewModel viewModel)
        {
            try
            {
                Articolo existing = null;
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var articoloId = int.Parse(id);
                    existing = Listino.Single(xx => xx.ArticoloId == articoloId);
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

                existing.Codice = viewModel.Codice;
                existing.Descrizione = viewModel.Descrizione;
                existing.CategoriaId = int.Parse(viewModel.Categoria);
                existing.Prezzo = viewModel.Prezzo;
                if (existing.Promozioni == null) existing.Promozioni = new PromozioniArticolo();
                existing.Promozioni.MetaPrezzo = viewModel.PromozioneMetaPrezzo;
                existing.Promozioni.InOfferta = viewModel.PromozioneInOfferta;

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
