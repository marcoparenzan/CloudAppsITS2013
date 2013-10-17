using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Company.DomainModels
{
    public class ListinoAggregateRoot
    {
        private IRepository<Articolo> _repository;

        public ListinoAggregateRoot(IRepository<Articolo> repository)
        {
            this._repository = repository;
        }

        public void CreaArticolo(
            string codice
            , string descrizione
            , decimal prezzo
            , bool inOfferta
            , bool metaPrezzo
            ) 
        {
            var existing = new Articolo();
            existing.Codice = codice;
            existing.Descrizione = descrizione;
            existing.CategoriaId = 1;
            existing.Prezzo = prezzo;
            existing.Promozioni = new PromozioniArticolo();
            existing.Promozioni.MetaPrezzo = metaPrezzo;
            existing.Promozioni.InOfferta = inOfferta;
            _repository.Add(existing);
        }

        public void ModificaArticolo(
            int articoloId
            , string codice
            , string descrizione
            , decimal prezzo
            , bool inOfferta
            , bool metaPrezzo
            )
        {
            var existing = _repository.Items.Single(xx => xx.ArticoloId == articoloId);

            existing.Codice = codice;
            existing.Descrizione = descrizione;
            existing.Prezzo = prezzo;
            if (existing.Promozioni == null) existing.Promozioni = new PromozioniArticolo();
            existing.Promozioni.MetaPrezzo = metaPrezzo;
            existing.Promozioni.InOfferta = inOfferta;

            _repository.Update(existing);
        }

        public Articolo Get(int id)
        {
            var item = _repository.Items.Single(xx => xx.ArticoloId == id);
            return item;
        }
    }
}
