using System;
using System.Collections.Generic;
using System.Text;
using BidApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BidApp.DataL.Map.Products
{
   public class ProductMap:EntityTypeConfiguration<ProductEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable<ProductEntity>("Product");
            builder.HasKey(x => x.Id);
            base.Configure(builder);

        }

    }
}
