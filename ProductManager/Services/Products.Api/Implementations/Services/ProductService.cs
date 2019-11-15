using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Abstrations;
using Products.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Entities;

namespace Products.Api.Services
{
    public class ProductService : IService<ProductViewModel>
    {
        private readonly IContextFactory _ContextFactory;
        private readonly IMapper<Product, ProductViewModel> _Mapper;
        public ProductService(IContextFactory contextFactory, IMapper<Product, ProductViewModel> mapper)
        {
            _ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public bool Delete(IEnumerable<string> ids)
        {
            using (var context = _ContextFactory.GetContext())
            {
                foreach (var id in ids)
                {
                    if (!context.Products.Any(c => c.Id.Equals(id)))
                    {
                        return false;
                    }

                    Product product = context.Products
                        .Where(c => c.Id == id)
                        .FirstOrDefault() ?? throw new NullReferenceException($"{nameof(product)} is null.");
                    context.Products.Remove(product);
                }
                context.SaveChanges();
                return true;
            }
        }

        public IEnumerable<ProductViewModel> Get()
        {
            using (var context = _ContextFactory.GetContext())
                return _Mapper.Map(context.Products.Include(p=>p.Category)).ToList();
        }

        public ProductViewModel Get(string id)
        {
            using (var context = _ContextFactory.GetContext())
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id), $"{nameof(id)} is null.");
                }

                Product product = context.Products
                    .Include(p=>p.Category)
                    .Where(p => p.Id.Equals(id))
                    .FirstOrDefault() ?? throw new NullReferenceException($"{nameof(product)} is null.");

                return 
                    new ProductViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        CategoryId = product.Category.Id
                    };
            }
        }


        public bool Post(IEnumerable<ProductViewModel> products)
        {
            using (var context = _ContextFactory.GetContext())
            {
                if (products == null)
                {
                    throw new ArgumentNullException(nameof(products), $"{nameof(products)} is null.");
                }

                var productsmapped = _Mapper.UnMap(products, context);
                foreach (var product in productsmapped)
                {
                    if (context.Products.Any(c => c.Id.Equals(product.Id)))
                    {
                        return false;
                    }
                    context.Products.Add(product);
                }
                context.SaveChanges();
                return true;
            }
        }
    }
}
