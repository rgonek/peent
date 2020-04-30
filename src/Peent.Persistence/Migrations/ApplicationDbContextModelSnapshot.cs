﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Peent.Persistence;

namespace Peent.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Peent.Domain.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.Property<int>("WorkspaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Peent.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Peent.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int>("WorkspaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Peent.Domain.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<int>("DecimalPlaces")
                        .HasColumnType("int")
                        .HasMaxLength(18);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Peent.Domain.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int>("WorkspaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Peent.Domain.Entities.TransactionAggregate.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Peent.Domain.Entities.TransactionAggregate.TransactionEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<long>("TransactionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionEntries");
                });

            modelBuilder.Entity("Peent.Domain.Entities.TransactionAggregate.TransactionTag", b =>
                {
                    b.Property<int?>("TagId")
                        .HasColumnType("int");

                    b.Property<long?>("TransactionId")
                        .HasColumnType("bigint");

                    b.HasKey("TagId", "TransactionId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionTag");
                });

            modelBuilder.Entity("Peent.Domain.Entities.Workspace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Workspaces");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Peent.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Peent.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Peent.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Peent.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Peent.Domain.Entities.Account", b =>
                {
                    b.HasOne("Peent.Domain.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Peent.Domain.Entities.Workspace", null)
                        .WithMany()
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "Created", b1 =>
                        {
                            b1.Property<int>("AccountId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("AccountId");

                            b1.HasIndex("ById");

                            b1.ToTable("Accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");
                        });

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "LastModified", b1 =>
                        {
                            b1.Property<int>("AccountId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("AccountId");

                            b1.HasIndex("ById");

                            b1.ToTable("Accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");
                        });
                });

            modelBuilder.Entity("Peent.Domain.Entities.Category", b =>
                {
                    b.HasOne("Peent.Domain.Entities.Workspace", null)
                        .WithMany()
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "Created", b1 =>
                        {
                            b1.Property<int>("CategoryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("CategoryId");

                            b1.HasIndex("ById");

                            b1.ToTable("Categories");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "LastModified", b1 =>
                        {
                            b1.Property<int>("CategoryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("CategoryId");

                            b1.HasIndex("ById");

                            b1.ToTable("Categories");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });
                });

            modelBuilder.Entity("Peent.Domain.Entities.Tag", b =>
                {
                    b.HasOne("Peent.Domain.Entities.Workspace", null)
                        .WithMany()
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "Created", b1 =>
                        {
                            b1.Property<int>("TagId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("TagId");

                            b1.HasIndex("ById");

                            b1.ToTable("Tags");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");

                            b1.WithOwner()
                                .HasForeignKey("TagId");
                        });

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "LastModified", b1 =>
                        {
                            b1.Property<int>("TagId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("TagId");

                            b1.HasIndex("ById");

                            b1.ToTable("Tags");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");

                            b1.WithOwner()
                                .HasForeignKey("TagId");
                        });
                });

            modelBuilder.Entity("Peent.Domain.Entities.TransactionAggregate.Transaction", b =>
                {
                    b.HasOne("Peent.Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "Created", b1 =>
                        {
                            b1.Property<long>("TransactionId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("TransactionId");

                            b1.HasIndex("ById");

                            b1.ToTable("Transactions");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");
                        });

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "LastModified", b1 =>
                        {
                            b1.Property<long>("TransactionId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("TransactionId");

                            b1.HasIndex("ById");

                            b1.ToTable("Transactions");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");
                        });
                });

            modelBuilder.Entity("Peent.Domain.Entities.TransactionAggregate.TransactionEntry", b =>
                {
                    b.HasOne("Peent.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Peent.Domain.Entities.TransactionAggregate.Transaction", "Transaction")
                        .WithMany("Entries")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "Created", b1 =>
                        {
                            b1.Property<long>("TransactionEntryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("TransactionEntryId");

                            b1.HasIndex("ById");

                            b1.ToTable("TransactionEntries");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");

                            b1.WithOwner()
                                .HasForeignKey("TransactionEntryId");
                        });

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "LastModified", b1 =>
                        {
                            b1.Property<long>("TransactionEntryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("TransactionEntryId");

                            b1.HasIndex("ById");

                            b1.ToTable("TransactionEntries");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");

                            b1.WithOwner()
                                .HasForeignKey("TransactionEntryId");
                        });

                    b.OwnsOne("Peent.Domain.ValueObjects.Money", "Money", b1 =>
                        {
                            b1.Property<long>("TransactionEntryId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Amount")
                                .HasColumnName("Amount")
                                .HasColumnType("decimal(38,18)");

                            b1.Property<int>("CurrencyId")
                                .HasColumnName("CurrencyId")
                                .HasColumnType("int");

                            b1.HasKey("TransactionEntryId");

                            b1.HasIndex("CurrencyId");

                            b1.ToTable("TransactionEntries");

                            b1.HasOne("Peent.Domain.Entities.Currency", "Currency")
                                .WithMany()
                                .HasForeignKey("CurrencyId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TransactionEntryId");
                        });
                });

            modelBuilder.Entity("Peent.Domain.Entities.TransactionAggregate.TransactionTag", b =>
                {
                    b.HasOne("Peent.Domain.Entities.Tag", "Tag")
                        .WithMany("TransactionTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Peent.Domain.Entities.TransactionAggregate.Transaction", "Transaction")
                        .WithMany("TransactionTags")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Peent.Domain.Entities.Workspace", b =>
                {
                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "Created", b1 =>
                        {
                            b1.Property<int>("WorkspaceId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("WorkspaceId");

                            b1.HasIndex("ById");

                            b1.ToTable("Workspaces");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");

                            b1.WithOwner()
                                .HasForeignKey("WorkspaceId");
                        });

                    b.OwnsOne("Peent.Domain.ValueObjects.AuditInfo", "LastModified", b1 =>
                        {
                            b1.Property<int>("WorkspaceId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ById")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("On")
                                .HasColumnType("datetime2");

                            b1.HasKey("WorkspaceId");

                            b1.HasIndex("ById");

                            b1.ToTable("Workspaces");

                            b1.HasOne("Peent.Domain.Entities.ApplicationUser", "By")
                                .WithMany()
                                .HasForeignKey("ById");

                            b1.WithOwner()
                                .HasForeignKey("WorkspaceId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
