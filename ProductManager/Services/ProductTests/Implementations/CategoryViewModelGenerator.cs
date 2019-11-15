using Bogus;
using Products.Api.ViewModel;
using System.Collections.Generic;
using System.Text;

namespace ProductTests.Implementations
{
    public class CategoryViewModelGenerator : IGenerator<CategoryViewModel>
    {
        private readonly Faker<CategoryViewModel> _Faker;

        public CategoryViewModelGenerator()
        {

            this._Faker = new Faker<CategoryViewModel>()
                .RuleFor(ev => ev.Id, faker => faker.Lorem.Sentence(3))
                .RuleFor(ev => ev.Name, faker => faker.Lorem.Sentence(3));
        }

        public CategoryViewModel Generate()
        {
            return this._Faker.Generate();
        }

        public IEnumerable<CategoryViewModel> GenerateMultiple(int count)
        {
            return this._Faker.Generate(count);
        }
    }
}
