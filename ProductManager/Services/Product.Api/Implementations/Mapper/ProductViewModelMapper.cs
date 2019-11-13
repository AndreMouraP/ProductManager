using Model.Contexts;
using Model.Entities;
using Product.Api.Abstrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Mapper
{
    public class ProductViewModelMapper<TDTO, TViewModel> : IMapper<TDTO, TViewModel>
        where TViewModel : ProductViewModel
        where TDTO : Model.Entities.Product
    {
        public IEnumerable<TViewModel> Map(IEnumerable<TDTO> products)
        {
            foreach (var product in products)
            {
                yield return (TViewModel) new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryId = product.Category.Id
                };
            }
        }

        public IEnumerable<TDTO> Map(IEnumerable<TViewModel> products, ProductContext productContext)
        {
            foreach (var product in products)
            {
                yield return (TDTO)new Model.Entities.Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = productContext.Categories.Where(c => c.Id.Equals(product.CategoryId))?.FirstOrDefault() ?? throw new Exception(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
            }
        }
    }
}
