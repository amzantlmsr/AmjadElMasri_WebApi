using API.Controllers;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


    public class CustomerCustomerContactContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public CustomerCustomerContactContext() : base("name=CustomerCustomerContactContext")
        {
        }

        public System.Data.Entity.DbSet<Model.Model.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Model.Model.CustomerContact> CustomerContacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                    //Configure default schema
                    modelBuilder.Entity<Customer>()
                    .Property(p => p.NumberOfEmployees)
                    .IsOptional();
            }
    
}
