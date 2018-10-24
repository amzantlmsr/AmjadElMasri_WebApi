using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using log4net;
using Model.Model;

namespace API.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        
        ILog log = LogManager.GetLogger(typeof(CustomersController));

        private CustomerCustomerContactContext _context = new CustomerCustomerContactContext();

        //private readonly CustomerCustomerContactContext _context;
        //public CustomersController (CustomerCustomerContactContext customerCustomerContactContext)
        //{
        //    _context = customerCustomerContactContext;
        //}  
        
        //public CustomersController()
        //{


        //}

        //private readonly ICustomerRepository _repository;

        //public CustomersController(ICustomerRepository repository)
        //{
        //    _repository = repository;
        //}

        //IQueryable<CustomerDetails>
        // GET: api/Customers
        [Route("")]
        public IHttpActionResult  GetCustomers()
        {
           if (_context.Customers.Count() == 0)
           {
                log.Error("the database elements are"+_context.Customers.Count());
                return NotFound();
           }
           else
           {
                var customers=_context.Customers.Select(x => new CustomerDetails { Id = x.Id, Title = x.Title, NumberOfEmployees = x.NumberOfEmployees });
                return Ok(customers);
           }
        }

        // GET: api/Customers/5
        [Route("{id:int}",Name= "GetCustomerById")]  
        [ResponseType(typeof(CustomerDetails))]
        public IHttpActionResult GetCustomerById(int id)
        {
            //Customer customer = _context.Customers.Find(id);
            // CustomerDetails customer = _repository.GetCustomerById(id);
            var customer = _context.Customers.Select(x => new CustomerDetails { Id = x.Id, Title = x.Title, NumberOfEmployees = x.NumberOfEmployees }).FirstOrDefault(p => p.Id == id);

            if (customer == null)
            {
                log.Error("the selection item does not exist");
                return NotFound();
            }

            return Ok(customer);
        }

        // GET: api/customers/contacts
        [HttpGet]
        [Route("contacts")]
        [ResponseType(typeof(CustomerCustomerContact))]
        public IHttpActionResult GetCustomerContacts()
        {
            var contacts = _context.CustomerContacts;
            var customerContact = _context.Customers.Join(contacts, cu => cu.Id, co => co.CustomerId, (cu, co) => new { cu, co }).Select(ccc => new CustomerCustomerContact()
            {
                CustomerId=ccc.cu.Id,
                Title=ccc.cu.Title,
                NumberOfEmployees=ccc.cu.NumberOfEmployees,
                CustomerContacts= new CustomerContactsDetails {FirstName=ccc.co.FirstName, LastName=ccc.co.LastName,Email=ccc.co.Email,CustomerId=ccc.cu.Id,Id=ccc.co.Id }
            });

            if (customerContact.Count() == 0)
            {
                log.Error("the items are"+ customerContact.Count());
                return BadRequest("the item does not exist");
            }

            return Ok(customerContact);
        }


        [HttpGet]
        [Route("contacts/{id=int}")]
        [ResponseType(typeof(CustomerCustomerContact))]
        public IHttpActionResult GetCustomerContactsById(int id)
        {
            var contacts = _context.CustomerContacts;
            var customerContact = _context.Customers.Join(contacts, cu => cu.Id, co => co.CustomerId, (cu, co) => new { cu, co }).Select(ccc => new CustomerCustomerContact()
            {
                CustomerId = ccc.cu.Id,
                Title = ccc.cu.Title,
                NumberOfEmployees = ccc.cu.NumberOfEmployees,
                CustomerContacts = new CustomerContactsDetails { FirstName = ccc.co.FirstName, LastName = ccc.co.LastName, Email = ccc.co.Email, CustomerId = ccc.cu.Id, Id = ccc.co.Id }
            }).Where(sss => sss.CustomerId == id);

            if (customerContact.Count()==0)
            {
                log.Error("the items are" + customerContact.Count());
                return BadRequest("the item does not exist");
            }

            return Ok(customerContact);
        }


        // PUT: api/Customers/5
        [HttpPut]
        [Route("edit/{id=int}")]
        [ResponseType(typeof(void))]
        //[ResponseType(typeof(CustomerDetails))]
        public HttpResponseMessage Update(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                log.Error("is not accepted");
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
            var item = _context.Customers.Find(id);
            if (item ==null)
            {
                log.Error("the customer does not exist");
                return Request.CreateResponse(HttpStatusCode.NotFound, id);        
            }
            item.Title = customer.Title;
            item.NumberOfEmployees = customer.NumberOfEmployees;
            //_context.Customers.
            //_context.Entry(customer).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    log.Error("the customer does not exist");
                    return Request.CreateResponse(HttpStatusCode.NotFound, id);
                }
                else
                {
                    log.Error("Can not save context");
                    //throw;
                }
            }
            //return CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer);
            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }


        // POST: api/Customers
        [HttpPost]
        [Route("add")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                log.Error("is not accepted");
                return BadRequest(ModelState);
            }

                _context.Customers.Add(new Customer ()
                {
                    Title=customer.Title,
                    NumberOfEmployees=customer.NumberOfEmployees
                });


            //_context.Customers.Add(customer);
            _context.SaveChanges();

            return CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer);
        }



        // DELETE: api/Customers/5
        [HttpDelete]
        [Route("delete/{id=int}")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = _context.Customers.Find(id);
            if (customer == null)
            {
                log.Error("doen not exist");
                return BadRequest("the item does not exist");
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Count(e => e.Id == id) > 0;
        }
    }
}