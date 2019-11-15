using Model.Contexts;
using Model.Entities;
using Abstrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Api.ViewModel;

namespace Products.Api.Mapper
{
    public class ProductViewModelMapper : IMapper<Product, ProductViewModel>
    {
        public IEnumerable<ProductViewModel> Map(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                yield return new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryId = product.Category.Id
                };
            }
        }

        public IEnumerable<Model.Entities.Product> UnMap(IEnumerable<ProductViewModel> products, ProductContext productContext)
        {
            foreach (var product in products)
            {
                yield return new Product
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
