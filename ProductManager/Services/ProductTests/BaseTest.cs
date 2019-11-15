using BogusMockGenerators.Abstractions;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Entities;
using Abstrations;
using ProductTests.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace ProductTests
{
    public abstract class BaseTest<TEntity,TViewmodel> 
        where TEntity : BaseEntity
    {
        protected IEntityGenerator<TEntity> Generator { get; set; }
        protected IGenerator<TViewmodel> ViewModelGenerator { get; set; }

        public BaseTest(IEntityGenerator<TEntity> generator, IGenerator<TViewmodel> viewModelGenerator)
        {
            this.Generator = generator;
            this.ViewModelGenerator = viewModelGenerator;
        }
        protected ProductContext GetContext(string name)
        {
            return new ProductContext(GetContextOptions(name));
        }

        protected DbContextOptions<ProductContext> GetContextOptions(string name)
        {
            var ctxOptionsBuilder = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(name);
            return ctxOptionsBuilder.Options;
        }

        protected abstract DbSet<TEntity> GetEntitiesDBSet(ProductContext ctx);

        protected abstract void AddEntity(TEntity entity, ProductContext ctx);

        protected abstract IService<TViewmodel> AddService();

        protected abstract void AddEntities(IEnumerable<TEntity> entities, ProductContext ctx);

        protected ArrangeResult<TEntity, TViewmodel> Arrange(string name)
        {
            var ctx = this.GetContext(name);

            return new ArrangeResult<TEntity, TViewmodel>
            {
                Context = ctx,
                Service = AddService(),
                Entities = GetEntitiesDBSet(ctx)
            };
        }

        [Fact]
        public void Test_TryCreateOne()
        {
            // Arrange
            var arrange = this.Arrange("Test_TryCreate");
            var service = arrange.Service;

            var viewmodel = ViewModelGenerator.GenerateMultiple(1);

            // Act
            var isEntityCreated = service.Post(viewmodel);

            // Assert
            Assert.True(isEntityCreated);
        }

        [Fact]
        public void Test_TryCreateMultiple()
        {
            // Arrange
            var arrange = this.Arrange("Test_TryCreate");
            var service = arrange.Service;

            var viewmodel = ViewModelGenerator.GenerateMultiple(10);

            // Act
            var isEntityCreated = service.Post(viewmodel);

            // Assert
            Assert.True(isEntityCreated);
        }


        [Fact]
        public void Test_GetAllWhere_Empty()
        {
            // Arrange
            var arrange = this.Arrange("Test_GetAllWhere_Empty");
            var service = arrange.Service;

            // Act
            var entitiesToCheck = service.Get();

            // Assert
            Assert.Empty(entitiesToCheck);
        }

    }
}
