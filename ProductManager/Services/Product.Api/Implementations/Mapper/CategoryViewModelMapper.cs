using Model.Contexts;
using Model.Entities;
using Product.Api.Abstrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Mapper
{
    public class CategoryViewModelMapper<TDTO,TTarget> : IMapper<TDTO,TTarget> 
        where TTarget : CategoryViewModel 
        where TDTO : Category
    { 
        public IEnumerable<TTarget> Map(IEnumerable<TDTO> categories)
        {
            foreach (var category in categories)
            {
                yield return (TTarget) new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                };
            }
        }

        public IEnumerable<TDTO> Map(IEnumerable<TTarget> categories, ProductContext productContext)
        {
            foreach (var category in categories)
            {
                yield return (TDTO)new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
            }
        }
    }
}
