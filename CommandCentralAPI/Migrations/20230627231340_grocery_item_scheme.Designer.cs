﻿// <auto-generated />
using CommandCentralAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CommandCentralAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230627231340_grocery_item_scheme")]
    partial class grocery_item_scheme
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CommandCentralAPI.dbmodels.DbGroceryList", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("household_idid")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("household_idid");

                    b.ToTable("grocery_list");
                });

            modelBuilder.Entity("CommandCentralAPI.dbmodels.DbGroceryListItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("grocery_list_idid")
                        .HasColumnType("integer");

                    b.Property<string>("item_amount")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("item_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("grocery_list_idid");

                    b.ToTable("grocery_list_item");
                });

            modelBuilder.Entity("CommandCentralAPI.dbmodels.DbHousehold", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.HasKey("id");

                    b.ToTable("household");
                });

            modelBuilder.Entity("CommandCentralAPI.dbmodels.DbGroceryList", b =>
                {
                    b.HasOne("CommandCentralAPI.dbmodels.DbHousehold", "household_id")
                        .WithMany()
                        .HasForeignKey("household_idid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("household_id");
                });

            modelBuilder.Entity("CommandCentralAPI.dbmodels.DbGroceryListItem", b =>
                {
                    b.HasOne("CommandCentralAPI.dbmodels.DbGroceryList", "grocery_list_id")
                        .WithMany()
                        .HasForeignKey("grocery_list_idid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("grocery_list_id");
                });
#pragma warning restore 612, 618
        }
    }
}
