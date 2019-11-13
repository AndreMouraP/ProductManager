using Bogus;
using Product.Api.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTests.Implementations
{
    public class CategoryViewModelGenerator : IViewModelGenerator<CategoryViewModel>
    {
        private readonly Faker<CategoryViewModel> faker;

        public CategoryViewModelGenerator()
        {

            this.faker = new Faker<CategoryViewModel>()
                .RuleFor(ev => ev.Id, faker => new Guid())
                .RuleFor(ev => ev.Name, faker => faker.Lorem.Sentence(3));
        }

        public CategoryViewModel Generate()
        {
            return this.faker.Generate();
        }

        public IEnumerable<CategoryViewModel> GenerateMultiple(int count)
        {
            return this.faker.Generate(count);
        }
    }
}
