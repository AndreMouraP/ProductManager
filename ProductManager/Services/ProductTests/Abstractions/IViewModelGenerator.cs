using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTests
{
    public interface IViewModelGenerator<TViewmodel> 
    {
        TViewmodel Generate();
        IEnumerable<TViewmodel> GenerateMultiple(int count);
    }
}
