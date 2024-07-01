﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductionRecipes.DataAccess.Contexts;

#nullable disable

namespace ProductionRecipes.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.27");

            modelBuilder.Entity("ProductionRecipes.Domain.Entities.AccionElements.AccionElement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AccionElements", (string)null);
                });

            modelBuilder.Entity("ProductionRecipes.Domain.Entities.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CompanyName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Shape")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("ProductionRecipes.Domain.Entities.Recipe.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Expertname")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ValidationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Recipes", (string)null);
                });

            modelBuilder.Entity("ProductionRecipes.Domain.Entities.AccionElements.Fases.Fase", b =>
                {
                    b.HasBaseType("ProductionRecipes.Domain.Entities.AccionElements.AccionElement");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("OperationId")
                        .HasColumnType("TEXT");

                    b.HasIndex("OperationId");

                    b.ToTable("Fases", (string)null);
                });

            modelBuilder.Entity("ProductionRecipes.Domain.Entities.AccionElements.Operations.Operation", b =>
                {
                    b.HasBaseType("ProductionRecipes.Domain.Entities.AccionElements.AccionElement");

                    b.Property<string>("UnityName")
                        .HasColumnType("TEXT");

                    b.ToTable("Operations", (string)null);
                });

            modelBuilder.Entity("ProductionRecipes.Domain.Entities.Recipe.Recipe", b =>
                {
                    b.HasOne("ProductionRecipes.Domain.Entities.Products.Product", "ProductToMake")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductToMake");
                });

            modelBuilder.Entity("ProductionRecipes.Domain.Entities.AccionElements.Fases.Fase", b =>
                {
                    b.HasOne("ProductionRecipes.Domain.Entities.AccionElements.AccionElement", null)
                        .WithOne()
                        .HasForeignKey("ProductionRecipes.Domain.Entities.AccionElements.Fases.Fase", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductionRecipes.Domain.Entities.AccionElements.Operations.Operation", null)
                        .WithMany("ExecFases")
                        .HasForeignKey("OperationId");
                });

            modelBuilder.Entity("ProductionRecipes.Domain.Entities.AccionElements.Operations.Operation", b =>
                {
                    b.HasOne("ProductionRecipes.Domain.Entities.AccionElements.AccionElement", null)
                        .WithOne()
                        .HasForeignKey("ProductionRecipes.Domain.Entities.AccionElements.Operations.Operation", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductionRecipes.Domain.Entities.AccionElements.Operations.Operation", b =>
                {
                    b.Navigation("ExecFases");
                });
#pragma warning restore 612, 618
        }
    }
}
