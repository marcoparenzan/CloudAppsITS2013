using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Company.DomainModels;

namespace Company.ReadModels
{
    public class ListinoDataContext
    {
        private IRepository<Articolo> _repository;

        public ListinoDataContext(IRepository<Articolo> repository)
        {
            this._repository = repository;
        }

        public PageOf<ArticoloDTO> Page(int pageNumber, int pageSize)
        {
            var query = _repository.Items;
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
            return page;
        }
    }
}
