using AlkusStore.Models.Configuration;
using AlkusStore.Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AlkusStore.Models.Identity
{
    public class WebContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public WebContext() : base("IdentityContextConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static WebContext Create()
        {
            return new WebContext();
        }
    }
}
