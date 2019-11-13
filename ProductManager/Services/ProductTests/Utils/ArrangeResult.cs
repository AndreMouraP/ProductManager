using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Entities;
using Product.Api.Abstrations;

namespace ProductTests.Utils
{
    public class ArrangeResult<TEntity,TService> 
        where TEntity : BaseEntity
    {
        public DbSet<TEntity> Entities { get; set; }

        public IService<TService> Service { get; set; }
        public ProductContext Context { get; set; }
    }
}
