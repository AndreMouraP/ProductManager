using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Abstrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Api.Factory
{
    public class ContextFactory : IContextFactory
    {
        public DbContextOptions<ProductContext> _ContextOptions;
        public ContextFactory(DbContextOptions<ProductContext> contextOptions)
        {
            _ContextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));
        }

        public ProductContext GetContext()
        {
            return new ProductContext(_ContextOptions);            
        }
    }
}
