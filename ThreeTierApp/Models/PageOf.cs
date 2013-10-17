using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThreeTierApp.Models
{
    public static class PageOfExtension
    {
        public static PageOf<T> Page<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return new PageOf<T>(query, pageNumber, pageSize);
        }
    }

    public class PageOf<T>
    {
        public PageOf(IQueryable<T> query, int pageNumber, int pageSize)
        {
            this.Items = 
                query
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .ToList();
            this.Total = query.Count();
            this.PageNumber = this.Total == 0 ? 0 : pageNumber + 1;
            this.PageSize = pageSize;
            this.PageCount = this.Total / this.PageSize + (this.Total % this.PageSize > 0 ? 1 : 0);
        }

        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int Total { get; set; }

        public int PageCount { get; set; }
    }
}