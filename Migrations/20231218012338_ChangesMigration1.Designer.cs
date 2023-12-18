﻿// <auto-generated />
using Agenda_Back.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Agenda_Back.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20231218012338_ChangesMigration1")]
    partial class ChangesMigration1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Agenda_Back.Entities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContactBookId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactBookId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Agenda_Back.Entities.ContactBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContactBookName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("OwnerUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("ContactBooks");
                });

            modelBuilder.Entity("Agenda_Back.Entities.SharedContactBook", b =>
                {
                    b.Property<int>("ContactBookId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("ContactBookId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("SharedContactBooks");
                });

            modelBuilder.Entity("Agenda_Back.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Agenda_Back.Entities.Contact", b =>
                {
                    b.HasOne("Agenda_Back.Entities.ContactBook", "ContactBook")
                        .WithMany("Contacts")
                        .HasForeignKey("ContactBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactBook");
                });

            modelBuilder.Entity("Agenda_Back.Entities.ContactBook", b =>
                {
                    b.HasOne("Agenda_Back.Entities.User", "OwnerUser")
                        .WithMany("MyContactBooks")
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerUser");
                });

            modelBuilder.Entity("Agenda_Back.Entities.SharedContactBook", b =>
                {
                    b.HasOne("Agenda_Back.Entities.ContactBook", "ContactBook")
                        .WithMany("SharedUsers")
                        .HasForeignKey("ContactBookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Agenda_Back.Entities.User", "User")
                        .WithMany("MySharedContactBooks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactBook");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Agenda_Back.Entities.ContactBook", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("SharedUsers");
                });

            modelBuilder.Entity("Agenda_Back.Entities.User", b =>
                {
                    b.Navigation("MyContactBooks");

                    b.Navigation("MySharedContactBooks");
                });
#pragma warning restore 612, 618
        }
    }
}