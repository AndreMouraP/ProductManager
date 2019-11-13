using BogusMockGenerators.Implementations;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Product.Api.Abstrations;
using Product.Api.Factory;
using Product.Api.Mapper;
using Product.Api.Services;
using ProductTests.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductTests
{
    public class ProductServiceTest: BaseTest<Model.Entities.Product, ProductViewModel>
    {
        public ProductServiceTest() : base(new ProductGenerator(), new ProductViewModelGenerator()) { }

        protected override void AddEntities(IEnumerable<Model.Entities.Product> entities, ProductContext ctx)
        {
            var category = entities.Select(p => p.Category);

            ctx
                .Categories
                .AddRange(category);
            ctx
                .Products
                .AddRange(entities);

            ctx.SaveChanges();
        }

        protected override void AddEntity(Model.Entities.Product entity, ProductContext ctx)
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
            return new ProductService<ProductViewModel>(
                    new ContextFactory(options),
                    new ProductViewModelMapper<Model.Entities.Product, ProductViewModel>()
                );            
        }

        protected override DbSet<Model.Entities.Product> GetEntitiesDBSet(ProductContext ctx)
        {
            return ctx.Products;
        }
    }
}
