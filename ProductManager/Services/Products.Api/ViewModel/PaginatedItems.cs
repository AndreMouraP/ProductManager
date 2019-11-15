using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Api.ViewModel
{
    public class PaginatedItems<T> where T : class
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public long Count { get; set; }

        public T Data { get; set; }

        public PaginatedItems(int pageIndex, int pageSize, long count, T data)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
        }
    }
}
