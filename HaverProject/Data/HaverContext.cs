using Microsoft.EntityFrameworkCore;
using HaverProject.Models;
using HaverProject.ViewModel;

namespace HaverProject.Data
{
    public class HaverContext : DbContext
    {
        public HaverContext(DbContextOptions<HaverContext> options)
            : base(options)
        {
        }
        public DbSet<NCR> Ncrs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<NCRPhoto> NCRPhotos { get; set; }

        public DbSet<EmailAddress> emailAddresses { get; set; }
        public DbSet<ItemProblem> ItemProblems { get; set; }
        public DbSet<SapNo> SapNos { get; set; }
        public DbSet<SupplierOrRecInsp> SupplierOrRecInsps { get; set; }
        public DbSet<ItemMarked> ItemMarkeds { get; set; }
        public DbSet<UseAsIs> UseAsIss { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<NCRComment> nCRComments { get; set; }
        public DbSet<NCRDetailsViewModel> nCRDetailsViewModels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Supplier entity
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id); // Use Name as primary key
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(255); // Example maximum length, adjust as necessary
            });
            modelBuilder.Entity<ItemProblem>(entity =>
            {
                entity.HasKey(e => e.Id); // Use Name as primary key
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(255); // Example maximum length, adjust as necessary
            });
            modelBuilder.Entity<SapNo>(entity =>
            {
                entity.HasKey(e => e.Id); // Use Name as primary key
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(255); // Example maximum length, adjust as necessary
            });
            modelBuilder.Entity<SupplierOrRecInsp>(entity =>
            {
                entity.HasKey(e => e.Id); // Use Name as primary key
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(255); // Example maximum length, adjust as necessary
            });
            modelBuilder.Entity<ItemMarked>(entity =>
            {
                entity.HasKey(e => e.Id); // Use Name as primary key
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(255); // Example maximum length, adjust as necessary
            });
            modelBuilder.Entity<UseAsIs>(entity =>
            {
                entity.HasKey(e => e.Id); // Use Name as primary key
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(255); // Example maximum length, adjust as necessary
            });
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id); // Use Name as primary key
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(255); // Example maximum length, adjust as necessary
            });
            modelBuilder.Ignore<NCRDetailsViewModel>();
        }

    }
}
