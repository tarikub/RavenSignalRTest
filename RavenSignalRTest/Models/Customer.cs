using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RavenSignalRTest.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public Customer() { }
    }
}