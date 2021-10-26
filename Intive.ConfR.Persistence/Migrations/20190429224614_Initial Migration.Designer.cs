﻿// <auto-generated />
using System;
using Intive.ConfR.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Intive.ConfR.Persistence.Migrations
{
    [DbContext(typeof(ConfRContext))]
    [Migration("20190429224614_Initial Migration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Intive.ConfR.Domain.PhotoUrl", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("Url");

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Intive.ConfR.Domain.PhotoUrl", b =>
                {
                    b.OwnsOne("Intive.ConfR.Domain.ValueObjects.EMailAddress", "RoomEmail", b1 =>
                        {
                            b1.Property<Guid>("PhotoUrlId");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnName("RoomEmail");

                            b1.HasKey("PhotoUrlId");

                            b1.ToTable("Photos");

                            b1.HasOne("Intive.ConfR.Domain.PhotoUrl")
                                .WithOne("RoomEmail")
                                .HasForeignKey("Intive.ConfR.Domain.ValueObjects.EMailAddress", "PhotoUrlId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
