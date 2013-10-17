using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DomainModels
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Update(T item);
        IQueryable<T> Items { get; }
    }
}
