using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public virtual Category Category { get; set; }
    }
}
