using BidApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.DataL.Map.Products
{
    public class ProductBidMap:EntityTypeConfiguration<ProductBidEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductBidEntity> builder)
        {
            builder.ToTable<ProductBidEntity>("ProductBid");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductBids).HasForeignKey(x => x.ProductId);

            base.Configure(builder);

        }

    }
}
