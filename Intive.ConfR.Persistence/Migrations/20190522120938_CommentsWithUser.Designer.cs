// <auto-generated />
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
    [Migration("20190522120938_CommentsWithUser")]
    partial class CommentsWithUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Intive.ConfR.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnName("Body");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnName("CreatedDateTime");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnName("LastModifiedDateTime");

                    b.Property<string>("UserDisplayName")
                        .IsRequired()
                        .HasColumnName("UserDisplayName");

                    b.HasKey("CommentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Intive.ConfR.Domain.Entities.PhotoUrl", b =>
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

            modelBuilder.Entity("Intive.ConfR.Domain.Entities.Comment", b =>
                {
                    b.OwnsOne("Intive.ConfR.Domain.ValueObjects.EMailAddress", "RoomEmail", b1 =>
                        {
                            b1.Property<Guid>("CommentId");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnName("RoomEmail");

                            b1.HasKey("CommentId");

                            b1.ToTable("Comments");

                            b1.HasOne("Intive.ConfR.Domain.Entities.Comment")
                                .WithOne("RoomEmail")
                                .HasForeignKey("Intive.ConfR.Domain.ValueObjects.EMailAddress", "CommentId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Intive.ConfR.Domain.Entities.PhotoUrl", b =>
                {
                    b.OwnsOne("Intive.ConfR.Domain.ValueObjects.EMailAddress", "RoomEmail", b1 =>
                        {
                            b1.Property<Guid>("PhotoUrlId");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnName("RoomEmail");

                            b1.HasKey("PhotoUrlId");

                            b1.ToTable("Photos");

                            b1.HasOne("Intive.ConfR.Domain.Entities.PhotoUrl")
                                .WithOne("RoomEmail")
                                .HasForeignKey("Intive.ConfR.Domain.ValueObjects.EMailAddress", "PhotoUrlId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
