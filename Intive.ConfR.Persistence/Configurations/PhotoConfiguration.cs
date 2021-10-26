using Intive.ConfR.Domain;
using Intive.ConfR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intive.ConfR.Persistence.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<PhotoUrl>

    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PhotoUrl> builder)
        {
            builder.Property(e => e.Id).HasColumnName("ID").IsRequired();

            builder.Property(e => e.Url).HasColumnName("Url").IsRequired();

            builder.OwnsOne(m => m.RoomEmail, a =>
            {
                a.Property(p => p.Value)
                .HasColumnName("RoomEmail")
                .IsRequired();

                a.Ignore(p => p.Domain);
                a.Ignore(p => p.Name);
            });
        }
    }
}
