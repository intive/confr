using System;
using Intive.ConfR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intive.ConfR.Persistence.Configurations
{
    class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(e => e.CreatedDateTime).HasColumnName("CommentID").IsRequired();

            builder.OwnsOne(m => m.RoomEmail, a =>
            {
                a.Property(p => p.Value)
                .HasColumnName("RoomEmail")
                .IsRequired();

                a.Ignore(p => p.Domain);
                a.Ignore(p => p.Name);
            });

            builder.Property(e => e.Body).HasColumnName("Body").IsRequired();

            builder.Property(e => e.CreatedDateTime).HasColumnName("CreatedDateTime").IsRequired();

            builder.Property(e => e.LastModifiedDateTime).HasColumnName("LastModifiedDateTime").IsRequired();

            builder.Property(e => e.UserDisplayName).HasColumnName("UserDisplayName").IsRequired();

            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired();
        }
    }
}
