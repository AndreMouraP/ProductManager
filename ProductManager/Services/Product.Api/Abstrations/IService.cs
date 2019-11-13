using Product.Api.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Abstrations
{
    public interface IService<TViewModel>
    {
        bool Post(IEnumerable<TViewModel> products);

        IEnumerable<TViewModel> Get();

        TViewModel Get(Guid id);
    }
}
