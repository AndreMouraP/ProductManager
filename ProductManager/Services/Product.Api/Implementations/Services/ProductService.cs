using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Product.Api.Abstrations;
using Product.Api.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Api.Services
{
    public class ProductService<TProduct> : IService<TProduct>
        where TProduct : ProductViewModel
    {
        private readonly IContextFactory ContextFactory;
        private readonly IMapper<Model.Entities.Product, ProductViewModel> Mapper;
        public ProductService(IContextFactory contextFactory, IMapper<Model.Entities.Product, ProductViewModel> mapper)
        {
            ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<TProduct> Get()
        {
            using (var context = ContextFactory.GetContext())
                return (IEnumerable<TProduct>)Mapper.Map(context.Products);
        }

        public TProduct Get(Guid id)
        {
            using (var context = ContextFactory.GetContext())
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id), $"{nameof(id)} is null.");
                }

                var product = context.Products.Where(p => p.Id.Equals(id)).FirstOrDefault();

                return (TProduct)
                    new ProductViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        CategoryId = product.Category.Id
                    };
            }
        }

        public bool Post(IEnumerable<TProduct> products)
        {
            using (var context = ContextFactory.GetContext())
            {
                if (products == null)
                {
                    throw new ArgumentNullException(nameof(products), $"{nameof(products)} is null.");
                }

                var productsmapped = Mapper.Map(products, context);
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
