using Model.Model;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public interface ICustomerRepository 
    {
        IQueryable<CustomerDetails> GetCustomers();
        CustomerDetails GetCustomerById(int id);
        IQueryable<CustomerCustomerContact> GetCustomerContacts();
        IQueryable<CustomerCustomerContact> GetCustomerContactsById(int id);
        Customer DeleteCustomer(int id);
        Customer Update(int id, Customer customer);
        Customer PostCustomer(Customer customer);
        void Dispose(bool disposing);
        bool CustomerExists(int id);
    }
    
}