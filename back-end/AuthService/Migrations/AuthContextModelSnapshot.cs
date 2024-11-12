﻿// <auto-generated />
using AuthService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthService.Migrations
{
    [DbContext(typeof(AuthContext))]
    partial class AuthContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("AuthService.Domain.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "admin@brand.com",
                            Name = "Admin",
                            Password = "BILqc4FrKQJMKrgEf8XfEMJhIHBtmkRQ5d2NQ1aUVnAFQZcq88aEcGspDKrXAAWf+j7/Bx+5jv83oYOeSuZyg/U8+a/mrlvJoLi+ZkMALa1RwTewDcIGH+hMRV0ikYwV7m7hQ6HVxwYOonAOplTaTVBoupuGrMKWoc+XBanxioEXPWwnVHMLGnixS4ginJx49h10WkzrVFnk1mfyduSWvyr8D+f4pIMx2XnPGbdfeKynCFQ2PIvwYNeR41wA8O1md1Q3M5mkJAc+ibLLaSGkP807KsGFvg2cmWK9MYf0MiNI6ZlkPUsxWwvZiW9YRBaKGYe4WuBDHgY+5Fa87/6E0rOYTC675/yUd2M7bp3c2xatUY/lJv6WQ83dzEp6YuFV5OJyTRAidDJEhrKcbUasrl6mFGfw2DRGKLPfokRAnFY="
                        });
                });
#pragma warning restore 612, 618
        }
    }
}