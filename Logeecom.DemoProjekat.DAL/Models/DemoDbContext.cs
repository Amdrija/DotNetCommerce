using Microsoft.EntityFrameworkCore;

namespace Logeecom.DemoProjekat.DAL.Models
{
    public class DemoDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(category =>
            {
                category.HasKey(cat => cat.Id);

                category.Property(cat => cat.Code)
                        .HasMaxLength(32)
                        .IsRequired();

                category.Property(cat => cat.Title)
                        .HasMaxLength(32)
                        .IsRequired();

                category.Property(cat => cat.Description)
                        .HasMaxLength(256)
                        .IsRequired();

                category.HasOne(cat => cat.ParentCategory)
                        .WithMany(cat => cat.Subcategories)
                        .HasForeignKey(cat => cat.ParentId)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.Restrict);

                category.HasIndex(cat => cat.Code)
                        .IsUnique();
            });

            modelBuilder.Entity<Product>(product =>
            {
                product.HasKey(p => p.Id);

                product.Property(p => p.SKU)
                        .HasMaxLength(32)
                        .IsRequired();

                product.Property(p => p.Title)
                        .HasMaxLength(32)
                        .IsRequired();

                product.Property(p => p.Brand)
                        .HasMaxLength(32)
                        .IsRequired();

                product.Property(p => p.Price)
                        .IsRequired();

                product.Property(p => p.ShortDescription)
                        .HasMaxLength(256)
                        .IsRequired();

                product.Property(p => p.Description)
                        .HasMaxLength(512)
                        .IsRequired();

                product.Property(p => p.Image)
                        .HasMaxLength(128)
                        .IsRequired();

                product.Property(p => p.Enabled)
                        .IsRequired();

                product.Property(p => p.Featured)
                        .IsRequired();

                product.Property(p => p.ViewCount)
                        .IsRequired();

                product.HasIndex(p => p.SKU)
                        .IsUnique();
            });

            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.Id);

                user.Property(u => u.Username)
                    .HasMaxLength(32)
                    .IsRequired();

                user.Property(u => u.Password)
                    .HasMaxLength(64)
                    .IsRequired();

                user.HasIndex(u => u.Username)
                    .IsUnique();
            });
        }
    }
}
