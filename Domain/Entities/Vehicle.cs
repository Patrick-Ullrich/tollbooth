using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public string LicensePlate { get; set; }
        public Customer Customer { get; set; }

    }
}
