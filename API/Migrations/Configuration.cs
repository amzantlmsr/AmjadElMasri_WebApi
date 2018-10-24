namespace API.Migrations
{
    using Model.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CustomerCustomerContactContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CustomerCustomerContactContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

                //context.Customers.AddOrUpdate(
                //  new Customer { Title = "Venetis",NumberOfEmployees=400 },
                //  new Customer { Title = "Sklavenitis",NumberOfEmployees=500 },
                //  new Customer { Title = "Eurobank",NumberOfEmployees=900 }
                //    );

                //context.SaveChanges();


                //context.CustomerContacts.AddOrUpdate(
                //    new CustomerContact { FirstName = "ioannis", LastName="Papadopoulos",Email="pap@gmail.com",CustomerId=7 },
                //    new CustomerContact { FirstName = "Dimitris", LastName = "Lazoa", Email = "laz@gmail.com", CustomerId = 8 },
                //    new CustomerContact { FirstName = "Olga", LastName = "Kiriazi", Email = "kir@gmail.com", CustomerId = 9 },
                //    new CustomerContact { FirstName = "Dimitra", LastName = "Papa", Email = "papa@gmail.com", CustomerId = 9 }
                //    );

                //context.SaveChanges();




        }
    }
}
