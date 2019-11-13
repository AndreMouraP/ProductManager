using Model.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Abstrations
{
    public interface IMapper<TSource,TTarget>
    {
        IEnumerable<TTarget> Map(IEnumerable<TSource> item);
        IEnumerable<TSource> Map(IEnumerable<TTarget> products, ProductContext productContext);
    }
}
