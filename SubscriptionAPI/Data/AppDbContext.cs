using Microsoft.EntityFrameworkCore;

namespace SubscriptionAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Models.Subscription> Subscriptions { get; set; }
        public DbSet<Models.Payment_History> Payment_Historys { get; set; }
        public DbSet<Models.User_Subscription> User_Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Models.Subscription>().HasData(new Models.Subscription
            {
                subscriptionId = 1,
                subscriptionTitle = "Basic",
                subscriptionPrice = 9.99,
                subscriptionDays = 30
            });

            modelBuilder.Entity<Models.Subscription>().HasData(new Models.Subscription
            {
                subscriptionId = 2,
                subscriptionTitle = "Premium",
                subscriptionPrice = 19.99,
                subscriptionDays = 100
            });

        }
    }

}
