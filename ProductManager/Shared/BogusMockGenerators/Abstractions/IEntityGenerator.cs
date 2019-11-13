using Model.Entities;
using System.Collections.Generic;

namespace BogusMockGenerators.Abstractions
{
    public interface IEntityGenerator<TEntity> where TEntity : BaseEntity
    {
        TEntity Generate();

        IEnumerable<TEntity> GenerateMultiple(int count);
    }
}
