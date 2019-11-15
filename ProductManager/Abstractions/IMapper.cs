using Model.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abstrations
{
    public interface IMapper<TSource,TTarget>
        where TTarget : class, new()
    {
        IEnumerable<TTarget> Map(IEnumerable<TSource> item);
        IEnumerable<TSource> UnMap(IEnumerable<TTarget> products, ProductContext context);
    }
}
