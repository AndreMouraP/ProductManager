using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Entities;
using Abstrations;
using Products.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Api.Services
{
    public class CategoryService : IService<CategoryViewModel> 
    {
        private readonly IContextFactory _ContextFactory;
        private readonly IMapper<Category, CategoryViewModel> _Mapper;
        public CategoryService(IContextFactory contextFactory, IMapper<Category, CategoryViewModel> mapper)
        {
            _ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public bool Delete(IEnumerable<string> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids), $"{nameof(ids)} is null.");
            }

            using (var context = _ContextFactory.GetContext())
            {
                foreach (var id in ids)
                {
                    if (!context.Categories.Any(c => c.Id.Equals(id)))
                    {
                        return false;
                    }

                    Category category = context.Categories.Where(c => c.Id == id).FirstOrDefault() ?? throw new NullReferenceException($"{nameof(category)} is null.");
                    context.Categories.Remove(category);
                }
                context.SaveChanges();
                return true;
            }
        
        }

        public IEnumerable<CategoryViewModel> Get()
        {
            using (var context = _ContextFactory.GetContext())
                return _Mapper.Map(context.Categories).ToList();
        }

        public CategoryViewModel Get(string id)
        {

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), $"{nameof(id)} is null.");
            }

            using (var context = _ContextFactory.GetContext())
            {

                Category category = context.Categories.Where(c => c.Id.Equals(id)).FirstOrDefault() ?? throw new NullReferenceException($"{nameof(category)} is null.");

                return
                    new CategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.Name
                    };
            }
          
        }

        public bool Post(IEnumerable<CategoryViewModel> categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories), $"{nameof(categories)} is null.");
            }

            using (var context = _ContextFactory.GetContext())
            {
                var categoriesmapped = _Mapper.UnMap(categories, context);
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
