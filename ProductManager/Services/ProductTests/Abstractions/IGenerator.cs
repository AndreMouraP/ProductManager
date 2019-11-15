using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTests
{
    public interface IGenerator<T> 
    {
        T Generate();
        IEnumerable<T> GenerateMultiple(int count);
    }
}
