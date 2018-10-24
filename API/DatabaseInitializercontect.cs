using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API
{
    public class DatabaseInitializercontect : DropCreateDatabaseAlways<CustomerCustomerContactContext>
    {

        protected override void Seed(CustomerCustomerContactContext context)
        {

            context.Customers.Add(
              new Customer() { Title = "amjad", NumberOfEmployees = 600 }
              //new Customer() { Title = "Sklavenitis", NumberOfEmployees = 500 },
              //new Customer() { Title = "Eurobank", NumberOfEmployees = 900 }
                );

            context.SaveChanges();

            //context.CustomerContacts.Add(
            //new CustomerContact { FirstName = "ioannis", LastName = "Papadopoulos", Email = "pap@gmail.com", CustomerId = 7 }
            //    //new CustomerContact { FirstName = "Dimitris", LastName = "Lazoa", Email = "laz@gmail.com", CustomerId = 8 },
            //    //new CustomerContact { FirstName = "Olga", LastName = "Kiriazi", Email = "kir@gmail.com", CustomerId = 9 },
            //    //new CustomerContact { FirstName = "Dimitra", LastName = "Papa", Email = "papa@gmail.com", CustomerId = 9 }
            //);

            // context.SaveChanges();



        }
    }
}