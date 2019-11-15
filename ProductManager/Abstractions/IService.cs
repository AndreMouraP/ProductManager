using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abstrations
{
    public interface IService<TViewModel>
    {
        bool Post(IEnumerable<TViewModel> products);

        IEnumerable<TViewModel> Get();

        TViewModel Get(string id);

        bool Delete(IEnumerable<string> ids);
    }
}
