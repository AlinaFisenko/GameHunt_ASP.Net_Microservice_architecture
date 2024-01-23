using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

namespace OrderAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
		public DbSet<Order_Devs>  Order_Devs{ get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			//modelBuilder.Entity<Order_Devs>().HasNoKey();
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().HasData(new Order
            {
                id_order = 1,
                id_user = "40f04aab-4afc-4984-a4bf-3c75e57d715d",
                title = "Test",
                genre = Genre.Action,
                platform = Platform.Windows,
                description = "Test",
                date_created = new DateTime(2021, 1, 1),
                budget = 1000,
                state = State.New,
                count_devs = 1,
                gameplay_time = new TimeSpan(1, 0, 0),
                deadline = new DateTime(2021, 1, 1),
                work_condition = WorkCondition.Ready_To_Take_Disabled,
                salary = 1000,
                job_title = Jobs.Developer
            });

        }
    }

}
