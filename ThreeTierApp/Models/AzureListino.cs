using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Company.DomainModels;

namespace ThreeTierApp.Models
{
    public class AzureListino: 
        //Microsoft.WindowsAzure.Storage.Table.DataServices.TableServiceContext, 
        IRepository<Articolo>
    {
        void IRepository<Articolo>.Add(Articolo item)
        {
            throw new NotImplementedException();
        }

        void IRepository<Articolo>.Update(Articolo item)
        {
            throw new NotImplementedException();
        }

        IQueryable<Articolo> IRepository<Articolo>.Items
        {
            get { throw new NotImplementedException(); }
        }
    }
}