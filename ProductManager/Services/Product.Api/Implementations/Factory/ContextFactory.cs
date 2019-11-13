using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Product.Api.Abstrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Factory
{
    public class ContextFactory : IContextFactory
    {
        private readonly DbContextOptions<ProductContext> ContextOptions;
        public ContextFactory(DbContextOptions<ProductContext> ContextOptions)
        {
            ContextOptions = ContextOptions ?? throw new ArgumentNullException(nameof(ContextOptions));
        }

        public ProductContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
            //optionsBuilder.UseSqlServer(ConnectionString);
            return new ProductContext(ContextOptions);            
        }
    }
}
