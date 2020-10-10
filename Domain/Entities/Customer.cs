using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Customer
    {
        public Customer()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Vehicle> Vehicles { get; private set; }

    }
}
