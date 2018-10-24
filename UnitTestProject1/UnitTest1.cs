using System;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Results;
using API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Model;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetCustomerById()
        {
            //Arrange

            // Set up Prerequisites   
            var controller = new CustomersController();
            // Act on Test  
            var response = controller.GetCustomerById(1);
            var contentResult = response as OkNegotiatedContentResult<Customer>;
            // Assert the result  
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);

        }

        [TestMethod]
        public void CustomerNotFound()
        {
            // Set up Prerequisites   
            var controller = new CustomersController();
            // Act  
            IHttpActionResult actionResult = controller.GetCustomerById(100);
            // Assert  
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }



        [TestMethod]
        public void PostCustomer()
        {
            var controller = new CustomersController();

            var item = GetDemoProduct();

            var result =
                controller.PostCustomer(item) as CreatedAtRouteNegotiatedContentResult<Customer>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "GetCustomerById");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Title, item.Title);
        }
        Customer GetDemoProduct()
        {
            return new Customer() { Id = 3, Title = "Venetis", NumberOfEmployees = 5 };
        }

    }
}
