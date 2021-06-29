using System;
using System.Collections.Generic;
using System.Text;
using BidApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BidApp.DataL.Map.Products
{
   public class ProductMap:EntityTypeConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable<Product>("Product");
            builder.HasKey(x => x.Id);
            base.Configure(builder);

        }

    }
}
