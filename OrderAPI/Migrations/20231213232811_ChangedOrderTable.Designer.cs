﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderAPI.Data;

#nullable disable

namespace OrderAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231213232811_ChangedOrderTable")]
    partial class ChangedOrderTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OrderAPI.Models.Order", b =>
                {
                    b.Property<int>("id_order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("budget")
                        .HasColumnType("double");

                    b.Property<short>("count_devs")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("date_created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("deadline")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("gameplay_time")
                        .HasColumnType("time(6)");

                    b.Property<int>("genre")
                        .HasColumnType("int");

                    b.Property<string>("id_user")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("job_title")
                        .HasColumnType("int");

                    b.Property<int>("platform")
                        .HasColumnType("int");

                    b.Property<double>("salary")
                        .HasColumnType("double");

                    b.Property<int>("state")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("work_condition")
                        .HasColumnType("int");

                    b.HasKey("id_order");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            id_order = 1,
                            budget = 1000.0,
                            count_devs = (short)1,
                            date_created = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            deadline = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            description = "Test",
                            gameplay_time = new TimeSpan(0, 1, 0, 0, 0),
                            genre = 0,
                            id_user = "40f04aab-4afc-4984-a4bf-3c75e57d715d",
                            job_title = 2,
                            platform = 0,
                            salary = 1000.0,
                            state = 0,
                            title = "Test",
                            work_condition = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
