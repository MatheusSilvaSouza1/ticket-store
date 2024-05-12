﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ticket-store-api")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Event.Events", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsPublished")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uuid");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "Domain.Event.Events.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<int?>("Number")
                                .HasColumnType("integer");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("DateRange", "Domain.Event.Events.DateRange#DateRange", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp with time zone");
                        });

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events", "ticket-store-api");
                });

            modelBuilder.Entity("Domain.Event.Sectors", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("integer");

                    b.Property<string>("PlaceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Sectors", "ticket-store-api");
                });

            modelBuilder.Entity("Domain.Organizer.Organizers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.Property<string>("CorporateReason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fantasy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Cnpj");

                    b.ToTable("Organizers", "ticket-store-api");
                });

            modelBuilder.Entity("Domain.Event.Events", b =>
                {
                    b.HasOne("Domain.Organizer.Organizers", null)
                        .WithMany()
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Event.Sectors", b =>
                {
                    b.HasOne("Domain.Event.Events", "Event")
                        .WithMany("Sectors")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Domain.Event.Events", b =>
                {
                    b.Navigation("Sectors");
                });
#pragma warning restore 612, 618
        }
    }
}
