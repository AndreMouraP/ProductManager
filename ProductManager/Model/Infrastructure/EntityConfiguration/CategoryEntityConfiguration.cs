using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Infrastructure.EntityConfiguration
{
    class CategoryEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Category
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasMaxLength(256).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UpdatedAt);
        }
    }
}
