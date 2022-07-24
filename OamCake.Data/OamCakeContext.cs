using Microsoft.EntityFrameworkCore;
using OamCake.Entity;

namespace OamCake.Data
{
    public class OamCakeContext : DbContext
    {
        public OamCakeContext(DbContextOptions<OamCakeContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedDbAsync();
        }

        public DbSet<Cake> Cake { get; set; }
        public DbSet<Catalog> Catalog { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<CustomCake> CustomCake { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryProvider> InventoryProvider { get; set; }
        public DbSet<MadeCake> MadeCake { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuPermit> MenuPermit { get; set; }
        public DbSet<MenuRolPermit> MenuRolPermit { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDelivery> OrderDelivery { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Permit> Permit { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Projection> Projection { get; set; }
        public DbSet<ProjectionDetail> ProjectionDetail { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Sell> Sell { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRol> UserRol { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
