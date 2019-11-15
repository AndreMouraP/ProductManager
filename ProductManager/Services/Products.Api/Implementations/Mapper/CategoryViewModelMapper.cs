using Model.Contexts;
using Model.Entities;
using Abstrations;
using System;
using System.Collections.Generic;
using Products.Api.ViewModel;

namespace Products.Api.Mapper
{
    public class CategoryViewModelMapper : IMapper<Category, CategoryViewModel>
    { 
        public IEnumerable<CategoryViewModel> Map(IEnumerable<Category> categories)
        {
            foreach (var category in categories)
            {
                yield return  new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                };
            }
        }

        public IEnumerable<Category> UnMap(IEnumerable<CategoryViewModel> categories, ProductContext productContext)
        {
            foreach (var category in categories)
            {
                yield return new Category
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
