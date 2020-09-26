using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private TollBoothDBContext _context;

        public CustomersController(TollBoothDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Customer> Get()
        {
            var customers = _context.Customers.ToList();
            return customers;
        }
    }
}
