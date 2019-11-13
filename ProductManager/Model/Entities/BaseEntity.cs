using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseEntity baseEntity))
            {
                return false;
            }

            return this.Id == baseEntity.Id
                && this.CreatedAt == baseEntity.CreatedAt
                && this.UpdatedAt == baseEntity.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.CreatedAt, this.UpdatedAt);
        }
    }
}
