﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubscriptionAPI.Data;

#nullable disable

namespace SubscriptionAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231212155548_UPDATEDdB")]
    partial class UPDATEDdB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SubscriptionAPI.Models.Subscription", b =>
                {
                    b.Property<ushort>("subscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint unsigned");

                    b.Property<int>("subscriptionDays")
                        .HasColumnType("int");

                    b.Property<double>("subscriptionPrice")
                        .HasColumnType("double");

                    b.Property<string>("subscriptionTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("subscriptionId");

                    b.ToTable("Subscriptions");

                    b.HasData(
                        new
                        {
                            subscriptionId = (ushort)1,
                            subscriptionDays = 30,
                            subscriptionPrice = 9.9900000000000002,
                            subscriptionTitle = "Basic"
                        },
                        new
                        {
                            subscriptionId = (ushort)2,
                            subscriptionDays = 100,
                            subscriptionPrice = 19.989999999999998,
                            subscriptionTitle = "Premium"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
