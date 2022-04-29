﻿// <auto-generated />
using System;
using AirlineManagement.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AirlineManagement.Migrations
{
    [DbContext(typeof(AirlineDbContext))]
    [Migration("20220429193021_InitialRelationship_AirlineInventory")]
    partial class InitialRelationship_AirlineInventory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AirlineManagement.Models.AirlineTbl", b =>
                {
                    b.Property<string>("AirlineNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContactAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UploadLogo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AirlineNo");

                    b.ToTable("airlineTbls");
                });

            modelBuilder.Entity("AirlineManagement.Models.InventoryTbl", b =>
                {
                    b.Property<string>("FlightNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AirlineNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BusinessClassSeat")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FromPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstrumentUsed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Meal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoOfRows")
                        .HasColumnType("int");

                    b.Property<int>("NonBusinessClassSeat")
                        .HasColumnType("int");

                    b.Property<string>("ScheduleDays")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TicketCost")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("ToPlace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FlightNumber");

                    b.HasIndex("AirlineNo");

                    b.ToTable("inventoryTbls");
                });

            modelBuilder.Entity("AirlineManagement.Models.InventoryTbl", b =>
                {
                    b.HasOne("AirlineManagement.Models.AirlineTbl", "Airlines")
                        .WithMany("Inventories")
                        .HasForeignKey("AirlineNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airlines");
                });

            modelBuilder.Entity("AirlineManagement.Models.AirlineTbl", b =>
                {
                    b.Navigation("Inventories");
                });
#pragma warning restore 612, 618
        }
    }
}
