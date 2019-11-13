using Bogus;
using Product.Api.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTests.Implementations
{
    public class ProductViewModelGenerator : IViewModelGenerator<ProductViewModel>
    {
        private readonly Faker<ProductViewModel> faker;
        public ProductViewModelGenerator()
        {
            var categoryGenerator = new CategoryViewModelGenerator();

            this.faker = new Faker<ProductViewModel>()
                .RuleFor(ev => ev.Id, faker => new Guid())
                .RuleFor(p => p.Name, faker => faker.Lorem.Sentence(3))
                .RuleFor(ev => ev.CategoryId, categoryGenerator.Generate().Id);
        }

        public ProductViewModel Generate()
        {
            return this.faker.Generate();
        }

        public IEnumerable<ProductViewModel> GenerateMultiple(int count)
        {
            return this.faker.Generate(count);
        }
    }
}
