using Bogus;
using BogusMockGenerators.Abstractions;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BogusMockGenerators.Implementations
{
    public class CategoryGenerator : IEntityGenerator<Category>
    {
        private readonly Faker<Category> faker;

        public CategoryGenerator()
        {

            this.faker = new Faker<Category>()
                .RuleFor(ev => ev.Name, faker => faker.Lorem.Sentence(3))
                .RuleFor(ev => ev.CreatedAt, faker => faker.Date.RecentOffset())
                .RuleFor(ev => ev.UpdatedAt, faker => faker.Date.RecentOffset());
        }

        public Category Generate()
        {
            return this.faker.Generate();
        }

        public IEnumerable<Category> GenerateMultiple(int count)
        {
            return this.faker.Generate(count);
        }
    }
}
