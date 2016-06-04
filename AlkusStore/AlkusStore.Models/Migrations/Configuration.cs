namespace AlkusStore.Models.Migrations
{
    using Entities;
    using Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebContext context)
        {
            if (!(context.Roles.Any(x => x.Name == "ADMIN")))
            {
                context.Roles.Add(new IdentityRole()
                {
                    Name = "ADMIN"
                });
            }

            if (!(context.Users.Any(u => u.UserName == "admin@admin.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    FirstName = "£ukasz",
                    LastName = "Zapa³a",
                    Address = "Poland",
                    Email = "zapala.lukasz@outlook.com",
                    UserName = "zapala.lukasz@outlook.com"
                };
                userManager.Create(userToInsert, "Password@123");
                userManager.AddToRole(context.Users.FirstOrDefault(x => x.UserName == userToInsert.UserName).Id, "ADMIN");
            }

            var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
            var imageSource = @"http://cdn3.saveritemedical.com/product-default.jpg";

            context.Products.AddOrUpdate(
                p => p.Id,
                // Beers
                new Product { Name = "Piwo £om¿a 500 ml", Category = "Piwo", Price = 2.58M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Piwo Tyskie 500 ml", Category = "Piwo", Price = 3.23M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Piwo ¯ywiec 500 ml", Category = "Piwo", Price = 3.34M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Piwo Kormoran Porter Warmiñski 500 ml", Category = "Piwo", Price = 8.49M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Piwo W¹socz 500 ml", Category = "Piwo", Price = 9.79M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Piwo Pilsner Urquell 500 ml", Category = "Piwo", Price = 5.99M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Piwo Pinta 500 ml", Category = "Piwo", Price = 7.99M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Piwo Reden 500 ml", Category = "Piwo", Price = 7.39M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Piwo Per³a 500 ml", Category = "Piwo", Price = .05M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Piwo Mi³os³aw 500 ml", Category = "Piwo", Price = 3.14M, Stock = 100, Description = description, ImagePath = imageSource },
                // Wine
                new Product { Name = "Wino Commandaria 750 ml", Category = "Wino", Price = 79.99M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Wino Grooteberg 750 ml", Category = "Wino", Price = 17.09M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Wino Marani 750 ml", Category = "Wino", Price = 26.39M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Wino Callia Alta 750 ml", Category = "Wino", Price = 79.99M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Wino Pure 750 ml", Category = "Wino", Price = 20.29M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Wino Lambrusco 750 ml", Category = "Wino", Price = 15.69M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Wino DE BORTOLI 750 ml", Category = "Wino", Price = 33.99M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Wino Emilie Durand 750 ml", Category = "Wino", Price = 62.99M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Wino Fresco 750 ml", Category = "Wino", Price = 11.99M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Wino Relumbron 750 ml", Category = "Wino", Price = 20.29M, Stock = 100, Description = description, ImagePath = imageSource },
                // Whisky
                new Product { Name = "Whisky Chivas Regal 700 ml", Category = "Whisky", Price = 248.00M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Whisky Ballantine's 700 ml", Category = "Whisky", Price = 119.00M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Whisky Bushmills 700 ml", Category = "Whisky", Price = 92.09M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Whisky Four Roses700 ml", Category = "Whisky", Price = 128.29M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Whisky Black Jack 700 ml", Category = "Whisky", Price = 59.99M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Whisky Grants 700 ml", Category = "Whisky", Price = 119.99M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Whisky Johnnie Walker 700 ml", Category = "Whisky", Price = 157.59M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Whisky Jim Beam 700 ml", Category = "Whisky", Price = 49.69M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Whisky Wild Turkey 700 ml", Category = "Whisky", Price = 94.59M, Stock = 100, Description = description, ImagePath = imageSource },
                new Product { Name = "Whisky Tullamore 700 ml", Category = "Whisky", Price = 154.59M, Stock = 100, Description = description, ImagePath = imageSource }
            );
        }
    }
}
