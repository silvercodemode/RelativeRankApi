using Microsoft.EntityFrameworkCore;
using RelativeRank.EntityFrameworkEntities;

namespace RelativeRank.Data
{
    public partial class RelativeRankContext : DbContext
    {
        public RelativeRankContext(DbContextOptions<RelativeRankContext> options) : base(options)
        {
        }

        public virtual DbSet<Show> Show { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserToShowMapping> UserToShowMapping { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Show>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("shows");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("users");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserToShowMapping>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.Showname });

                entity.ToTable("user_to_show_mapping");

                entity.HasIndex(e => new { e.Username, e.Rank })
                    .HasName("uq_rank_unique_to_user")
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Showname)
                    .HasColumnName("showname")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Rank).HasColumnName("rank");

                entity.HasOne(d => d.ShownameNavigation)
                    .WithMany(p => p.UserToShowMapping)
                    .HasForeignKey(d => d.Showname)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_to_s__shown__0D7A0286");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.UserToShowMapping)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_to_s__usern__0C85DE4D");
            });
        }
    }
}
