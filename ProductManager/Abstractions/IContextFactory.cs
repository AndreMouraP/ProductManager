using Model.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abstrations
{
    public interface IContextFactory
    {
        ProductContext GetContext();
    }
}
