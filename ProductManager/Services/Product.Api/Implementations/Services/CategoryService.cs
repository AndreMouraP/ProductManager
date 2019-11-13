using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Entities;
using Product.Api.Abstrations;
using Product.Api.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Services
{
    public class CategoryService<TCategory> : IService<TCategory> 
        where TCategory : CategoryViewModel
    {
        private readonly IContextFactory ContextFactory;
        private readonly IMapper<Category, CategoryViewModel> Mapper;
        public CategoryService(IContextFactory contextFactory, IMapper<Category, CategoryViewModel> mapper)
        {
            ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<TCategory> Get()
        {
            using (var context = ContextFactory.GetContext())
                return (IEnumerable<TCategory>)Mapper.Map(context.Categories);
        }

        public TCategory Get(Guid id)
        {
            using (var context = ContextFactory.GetContext())
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id), $"{nameof(id)} is null.");
                }

                var category = context.Categories.Where(c => c.Id.Equals(id)).FirstOrDefault() ?? throw new ArgumentNullException(nameof(id), $"{nameof(id)} is null."); ;

                return (TCategory)
                    new CategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.Name
                    };
            }
        }

        public bool Post(IEnumerable<TCategory> categories)
        {
            using (var context = ContextFactory.GetContext())
            {

                if (categories == null)
                {
                    throw new ArgumentNullException(nameof(categories), $"{nameof(categories)} is null.");
                }

                var categoriesmapped = Mapper.Map(categories, context);
                foreach (var category in categoriesmapped)
                {
                    if (context.Categories.Any(c => c.Id.Equals(category.Id)))
                    {
                        return false;
                    }
                    context.Categories.Add(category);
                }
                context.SaveChanges();
                return true;
            }
        }
    }
}
