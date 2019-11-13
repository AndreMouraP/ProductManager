using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Infrastructure.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Contexts
{
    public class ProductContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration<Product>());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration<Category>());
        }
    }
}
