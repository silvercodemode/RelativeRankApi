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
            modelBuilder.UseIdentityColumns();

            modelBuilder.Entity<Show>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("shows");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("users");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasColumnName("password");

                entity.Property(e => e.PasswordSalt)
                    .HasColumnName("password_salt");
            });

            modelBuilder.Entity<UserToShowMapping>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ShowId });

                entity.ToTable("user_to_show_mapping");

                entity.HasIndex(e => new { e.UserId, e.Rank })
                    .HasName("uq_rank_unique_to_user")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("userid")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ShowId)
                    .HasColumnName("showid")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Rank).HasColumnName("rank");

                entity.HasOne(d => d.ShowNavigation)
                    .WithMany(p => p.UserToShowMapping)
                    .HasForeignKey(d => d.ShowId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.UserToShowMapping)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
