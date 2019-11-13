using Bogus;
using BogusMockGenerators.Abstractions;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BogusMockGenerators.Implementations
{
    public class ProductGenerator : IEntityGenerator<Product>
    {
        private readonly Faker<Product> faker;

        public ProductGenerator()
        {
            var gategoryGenerator = new CategoryGenerator();

            this.faker = new Faker<Product>()
                .RuleFor(p => p.Name, faker => faker.Lorem.Sentence(3))
                .RuleFor(ev => ev.Category, gategoryGenerator.Generate())
                .RuleFor(p => p.CreatedAt, faker => faker.Date.RecentOffset())
                .RuleFor(p => p.UpdatedAt, faker => faker.Date.RecentOffset());
        }

        public Product Generate()
        {
            return this.faker.Generate();
        }

        public IEnumerable<Product> GenerateMultiple(int count)
        {
            return this.faker.Generate(count);
        }
    }
}
