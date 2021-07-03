using BidApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.DataL.Map.Users
{
    public class UserMap : EntityTypeConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable<UserEntity>("User");
            builder.HasKey(x => x.Id);
            base.Configure(builder);

        }

    }
}
