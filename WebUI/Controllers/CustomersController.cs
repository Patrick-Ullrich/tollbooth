using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Customers;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using WebUI.Middleware;

namespace WebUI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersService _customerService;

        public CustomersController(CustomersService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        // 3. Different types of Status Codes with ProduceResponseType
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorMessage), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            return Ok(customer);
        }
    }
}
