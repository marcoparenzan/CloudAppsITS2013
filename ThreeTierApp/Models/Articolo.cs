using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThreeTierApp.Models
{
    public partial class Articolo
    {
        public int ArticoloId { get; set; }
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public int CategoriaId { get; set; }

        public PromozioniArticolo Promozioni { get; set; }
    }

    public partial class Articolo
    {
        public decimal Prezzo { get; set; }
    }

    public class PromozioniArticolo
    {
        public bool InOfferta { get; set; }
        public bool MetaPrezzo { get; set; }
    }
}