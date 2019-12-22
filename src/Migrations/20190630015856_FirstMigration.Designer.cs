﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RelativeRank.Data;

namespace RelativeRank.Migrations
{
    [DbContext(typeof(RelativeRankContext))]
    [Migration("20190630015856_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RelativeRank.EntityFrameworkEntities.Show", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Name");

                    b.ToTable("shows");
                });

            modelBuilder.Entity("RelativeRank.EntityFrameworkEntities.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Username");

                    b.ToTable("users");
                });

            modelBuilder.Entity("RelativeRank.EntityFrameworkEntities.UserToShowMapping", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasMaxLength(32)
                        .IsUnicode(false);

                    b.Property<string>("Showname")
                        .HasColumnName("showname")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<short>("Rank")
                        .HasColumnName("rank");

                    b.HasKey("Username", "Showname");

                    b.HasIndex("Showname");

                    b.HasIndex("Username", "Rank")
                        .IsUnique()
                        .HasName("uq_rank_unique_to_user");

                    b.ToTable("user_to_show_mapping");
                });

            modelBuilder.Entity("RelativeRank.EntityFrameworkEntities.UserToShowMapping", b =>
                {
                    b.HasOne("RelativeRank.EntityFrameworkEntities.Show", "ShownameNavigation")
                        .WithMany("UserToShowMapping")
                        .HasForeignKey("Showname")
                        .HasConstraintName("FK__user_to_s__shown__0D7A0286");

                    b.HasOne("RelativeRank.EntityFrameworkEntities.User", "UsernameNavigation")
                        .WithMany("UserToShowMapping")
                        .HasForeignKey("Username")
                        .HasConstraintName("FK__user_to_s__usern__0C85DE4D");
                });
#pragma warning restore 612, 618
        }
    }
}
