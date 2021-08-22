using FinansApp.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace FinansApp.Data
{
    public class FinansAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Signal> Signals { get; set; }
        public DbSet<SignalRequest> SignalRequests { get; set; }
        public DbSet<StaticPage> StaticPages { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Portfoy> Portfoys { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = "Server=.;Database=FinansAppDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            var connectionString = "Server=194.31.59.42;initial catalog=FinansAppDb;persist security info=True;user id=rgokcan;password=10Rid29_;multipleactiveresultsets=True;";
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        public FinansAppDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alert>().Property(x => x.Limit).HasPrecision(16, 8);
            modelBuilder.Entity<Portfoy>().Property(x => x.Amount).HasPrecision(16, 8);
            modelBuilder.Entity<Portfoy>().Property(x => x.Price).HasPrecision(16, 8);
        }
    }
}