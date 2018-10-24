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
    [RoutePrefix("api/contacts")]
    public class CustomerContactsController : ApiController
    {
        ILog log = LogManager.GetLogger(typeof(CustomerContactsController));
        private CustomerCustomerContactContext _context = new CustomerCustomerContactContext();

        //private readonly CustomerCustomerContactContext _context;

        //public CustomerContactsController(CustomerCustomerContactContext customerCustomerContactContext)
        //{
        //    _context = customerCustomerContactContext;

        //}
        // GET: api/CustomerContacts
        [Route("")]
        public IHttpActionResult GetContacts()
        {
            if (_context.CustomerContacts.Count() == 0)
            {
                log.Error("the database elements are" + _context.CustomerContacts.Count());
                return NotFound();
            }
            else
            {
                var contacts=_context.CustomerContacts.Select(x => new CustomerContactsDetails { FirstName = x.FirstName, LastName = x.LastName, Email = x.Email, Id = x.Id, CustomerId = x.CustomerId });
                return Ok(contacts);
            }

        }

        // GET: api/CustomerContacts/5
        [Route("{id:int}", Name = "GetContactById")]
        [ResponseType(typeof(CustomerContactsDetails))]
        public IHttpActionResult GetContactById(int id)
        {
            var contact = _context.CustomerContacts.Select(x => new CustomerContactsDetails { FirstName = x.FirstName, LastName = x.LastName, Email = x.Email, Id = x.Id, CustomerId = x.CustomerId }).FirstOrDefault(p => p.Id== id);

            if (contact == null)
            {
                log.Error("the contact does not exist");
                return BadRequest("the item does not exist");
            }

            return Ok(contact);
        }


        [HttpGet]
        [Route("customer")]
        [ResponseType(typeof(CustomerContactCustomer))]
        public IHttpActionResult GetContactCustomer()
        {
            var customers = _context.Customers;
            var customerContact = _context.CustomerContacts.Join(customers, cu => cu.CustomerId, co => co.Id, (cu, co) => new { cu, co }).Select(ccc => new CustomerContactCustomer()
            {
                Id = ccc.cu.Id,
                FirstName = ccc.cu.FirstName,
                LastName = ccc.cu.LastName,
                Email = ccc.cu.Email,
                CustomerDetails = new CustomerDetails { Title = ccc.co.Title, NumberOfEmployees = ccc.co.NumberOfEmployees, Id = ccc.co.Id }
            });

            if (customerContact.Count() == 0)
            {
                log.Error("the items are" + customerContact.Count());
                return BadRequest("the item does not exist");
            }

            return Ok(customerContact);
        }


        [HttpGet]
        [Route("customer/{id=int}")]
        [ResponseType(typeof(CustomerContactCustomer))]
        public IHttpActionResult GetContactCustomerById( int id)
        {
            var customers = _context.Customers;
            var customerContact = _context.CustomerContacts.Join(customers, cu => cu.CustomerId, co => co.Id, (cu, co) => new { cu, co }).Select(ccc => new CustomerContactCustomer()
            {
                Id = ccc.cu.Id,
                FirstName = ccc.cu.FirstName,
                LastName = ccc.cu.LastName,
                Email = ccc.cu.Email,
                CustomerDetails = new CustomerDetails { Title = ccc.co.Title, NumberOfEmployees = ccc.co.NumberOfEmployees, Id = ccc.co.Id }
            }).Where(sss => sss.Id == id); ;

            if (customerContact.Count() == 0)
            {
                log.Error("the selected id is" + customerContact.Count());
                return BadRequest("the item does not exist");
            }

            return Ok(customerContact);
        }


        // PUT: api/Customers/5
        [HttpPut]
        [Route("edit/{id=int}")]
        [ResponseType(typeof(void))]
        //[ResponseType(typeof(CustomerDetails))]
        public HttpResponseMessage UpdateContact(int id, CustomerContact customerContact)
        {
            if (!ModelState.IsValid)
            {
                log.Error("is not accepted");
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
            var item = _context.CustomerContacts.Find(id);
            if (item == null)
            {
                log.Error("the contact does not exist");
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            item.FirstName = customerContact.FirstName;
            item.LastName = customerContact.LastName;
            item.Email = customerContact.Email;
            item.CustomerId = customerContact.CustomerId;

            //_context.Customers.
            //_context.Entry(customer).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                if (!CustomerContactExists(id))
                {
                    log.Error("the contact does not exist");
                    return Request.CreateResponse(HttpStatusCode.NotFound, id);
                }
                else
                {
                    log.Error("Can not save context");
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable);
                    //throw ex;
                }
            }
            //return CreatedAtRoute("GetContactById", new { id = customerContact.Id }, customerContact);
            return Request.CreateResponse(HttpStatusCode.OK, customerContact);
        }
        
        // POST: api/Contacts/5
        [HttpPost]
        [Route("add")]
        [ResponseType(typeof(CustomerContact))]
        public IHttpActionResult PostContact(CustomerContact customerContact)
        {
            if (!ModelState.IsValid)
            {
                log.Error("is not accepted");
                return BadRequest(ModelState);
            }

            //_context.CustomerContacts.Add(new CustomerContact()
            //{
            //   FirstName = customerContact.FirstName,
            //    LastName = customerContact.LastName,
            //    Email = customerContact.Email,
            //    CustomerId = customerContact.CustomerId
            //});

            try
            {
                _context.CustomerContacts.Add(customerContact);
                _context.SaveChanges();
            }

            catch (Exception)
            {
                log.Error("can not save context");
                return BadRequest("There is no customer");
                //throw ex;
            }

            return CreatedAtRoute("GetContactById", new { id = customerContact.Id }, customerContact);
        }

        [HttpDelete]
        [Route("delete/{id=int}")]
        [ResponseType(typeof(CustomerContact))]
        public IHttpActionResult DeleteContact(int id)
        {
            CustomerContact contact = _context.CustomerContacts.Find(id);
            if (contact == null)
            {
                log.Error("the contact does not exist");
                return BadRequest("the item does not exist");
            }

            _context.CustomerContacts.Remove(contact);
            _context.SaveChanges();

            return Ok(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerContactExists(int id)
        {
            return _context.CustomerContacts.Count(e => e.Id == id) > 0;
        }

    }
}