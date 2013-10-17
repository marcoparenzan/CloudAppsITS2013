using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Company.ViewModels
{
    public class ArticoloViewModel
    {
        [Description("")]
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public decimal Prezzo { get; set; }
        public string Categoria { get; set; }
        public bool PromozioneInOfferta { get; set; }
        public bool PromozioneMetaPrezzo { get; set; }

        public bool CategoryFixed { get; set; }
    }
}