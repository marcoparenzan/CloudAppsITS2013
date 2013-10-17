using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Company.DomainModels;
using Company.ReadModels;
using Company.ViewModels;
using ThreeTierApp.Models;

namespace ThreeTierApp.Controllers
{
    public class ListinoController : RepositoryArticoloController
    {
        private IRepository<Articolo> _repository;
        private ListinoAggregateRoot _aggregateRoot;
        private ListinoDataContext _dataContext;

        public ListinoController()
        {
            _repository = this; // ; //  new SessionListino();
            _aggregateRoot = new ListinoAggregateRoot(_repository);
            _dataContext = new ListinoDataContext(_repository);
        }

        public ActionResult Index(int pageNumber = 0, int pageSize = 10)
        {
            var page = _dataContext.Page(pageNumber, pageSize);
            return View(page);
        }

        public ActionResult Json(int pageNumber = 0, int pageSize = 10)
        {
            var page = _dataContext.Page(pageNumber, pageSize);
            return Json(page, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id, int? categoryId)
        {
            Articolo item = null;
            if (id.HasValue)
            {
                item = _aggregateRoot.Get(id.Value);
            }
            else
            {
                item = new Articolo();
                if (categoryId.HasValue)
                    item.CategoriaId = categoryId.Value;
            }
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

        [HttpPost]
        public ActionResult Edit(string id, ArticoloViewModel viewModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    _aggregateRoot.ModificaArticolo(
                        int.Parse(id)
                        ,
                        viewModel.Codice
                        ,
                        viewModel.Descrizione
                        ,
                        viewModel.Prezzo
                        ,
                        viewModel.PromozioneInOfferta
                        ,
                        viewModel.PromozioneMetaPrezzo
                    );
                }
                else
                {
                    _aggregateRoot.CreaArticolo(
                      viewModel.Codice
                      ,
                      viewModel.Descrizione
                      ,
                      viewModel.Prezzo
                      ,
                      viewModel.PromozioneInOfferta
                      ,
                      viewModel.PromozioneMetaPrezzo
                  );
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
