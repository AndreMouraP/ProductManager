using Abstrations;
using BogusMockGenerators.Implementations;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Entities;
using Products.Api.Factory;
using Products.Api.Mapper;
using Products.Api.Services;
using Products.Api.ViewModel;
using ProductTests.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProductTests
{
    public class ProductServiceTest: BaseTest<Product, ProductViewModel>
    {
        public ProductServiceTest() : base(
            new ProductGenerator(new ProductContext(new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase("ProductServiceTest").Options)),
            new ProductViewModelGenerator(new ProductContext(new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase("ProductServiceTest").Options))) { }

        protected override void AddEntities(IEnumerable<Product> entities, ProductContext ctx)
        {
            foreach (Product prod in entities)
            {
                var category = entities.Select(p => p.Category);

                ctx
                    .Categories
                    .AddRange(category);
                ctx
                    .Products
                    .AddRange(entities);
            }
           
            ctx.SaveChanges();
        }

        protected override void AddEntity(Product entity, ProductContext ctx)
        {
            var category = entity.Category;

            ctx
                .Categories
                .Add(category);
            ctx
                .Products
                .Add(entity);

            ctx.SaveChanges();
        }

        protected override IService<ProductViewModel> AddService()
        {
            var options = GetContextOptions("ProductServiceTest");
            return new ProductService(
                    new ContextFactory(options),
                    new ProductViewModelMapper()
                );            
        }

        protected override DbSet<Model.Entities.Product> GetEntitiesDBSet(ProductContext ctx)
        {
            return ctx.Products;
        }
    }
}
