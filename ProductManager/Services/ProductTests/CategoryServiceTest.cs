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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ProductTests
{
    public class CategoryServiceTest : BaseTest<Category, CategoryViewModel>
    {
        public CategoryServiceTest() : base(new BogusMockGenerators.Implementations.CategoryGenerator(), new CategoryViewModelGenerator()) { }

        protected override void AddEntities(IEnumerable<Category> entities, ProductContext ctx)
        {
            foreach(Category category in entities)
                ctx
                    .Categories
                    .AddRange(category);
  
            ctx.SaveChanges();
        }

        protected override void AddEntity(Category entity, ProductContext ctx)
        {
            ctx
                    .Categories
                    .AddRange(entity);

            ctx.SaveChanges();
        }

        protected override IService<CategoryViewModel> AddService()
        {
            var options = GetContextOptions("ProductServiceTest");
            return new CategoryService(
                    new ContextFactory(options),
                    new CategoryViewModelMapper()
                );
        }

        protected override DbSet<Category> GetEntitiesDBSet(ProductContext ctx)
        {
            return ctx.Categories;
        }

    }
}
