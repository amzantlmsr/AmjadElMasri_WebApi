using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class CustomerCustomerContact
    {

        //public int Contact_Id { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string Email { get; set; }

        public int CustomerId { get; set; }

        public string Title { get; set; }

        public int NumberOfEmployees { get; set; }

        public CustomerContactsDetails CustomerContacts  {get;set;}
    }
}
