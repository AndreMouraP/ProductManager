using Bogus;
using BogusMockGenerators.Implementations;
using Model.Contexts;
using Model.Entities;
using Products.Api.ViewModel;
using System.Collections.Generic;
using System.Text;

namespace ProductTests.Implementations
{
    public class ProductViewModelGenerator : IGenerator<ProductViewModel>
    {
        private readonly Faker<ProductViewModel> _Faker;
        public ProductViewModelGenerator(ProductContext ctx)
        {
            Category category = new CategoryGenerator().Generate();
            ctx.Categories.Add(category);
            ctx.SaveChanges();

            this._Faker = new Faker<ProductViewModel>()
                .RuleFor(ev => ev.Id, faker => faker.Lorem.Sentence(3))
                .RuleFor(p => p.Name, faker => faker.Lorem.Sentence(3))
                .RuleFor(ev => ev.CategoryId, category.Id);
        }

        public ProductViewModel Generate()
        {
            return this._Faker.Generate();
        }

        public IEnumerable<ProductViewModel> GenerateMultiple(int count)
        {
            return this._Faker.Generate(count);
        }
    }
}
