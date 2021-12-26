﻿// <auto-generated />
using System;
using Conch.Stock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Conch.Stock.Migrations
{
    [DbContext(typeof(StockDBContext))]
    partial class StockDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Conch.Stock.StockGoods", b =>
                {
                    b.Property<Guid>("StockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GoodsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FreezeNum")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.Property<string>("GoodsName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Num")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.Property<Guid?>("StockMasterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StockId", "GoodsId");

                    b.HasIndex("StockMasterId");

                    b.ToTable("StockGoods");
                });

            modelBuilder.Entity("Conch.Stock.StockMaster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("StockMaster");
                });

            modelBuilder.Entity("Conch.Stock.StockGoods", b =>
                {
                    b.HasOne("Conch.Stock.StockMaster", null)
                        .WithMany("GoodsList")
                        .HasForeignKey("StockMasterId");
                });

            modelBuilder.Entity("Conch.Stock.StockMaster", b =>
                {
                    b.Navigation("GoodsList");
                });
#pragma warning restore 612, 618
        }
    }
}
