using Model.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Abstrations
{
    public interface IContextFactory
    {
        ProductContext GetContext();
    }
}
