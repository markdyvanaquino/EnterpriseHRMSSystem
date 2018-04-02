using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<LibraryManagement.Models.BookModel> Books { get; set; }

        public DbSet<LibraryManagement.Models.TransactionModel> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TransactionType to store as a string
            modelBuilder.Entity<TransactionModel>()
                .Property(t => t.TransactionType)
                .HasConversion<string>();

            //modelBuilder.Entity<TransactionModel>()
            //    .HasOne(th => th.User)
            //    .WithMany()
            //    .HasForeignKey(th => th.UserID);

        }
    }
}
