using Application.Common.Abstracts;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Customers
{
    public class CustomersService : ServiceBase
    {
        private readonly ITollBoothDBContext _context;

        public CustomersService(ITollBoothDBContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer), id);
            }

            return customer;
        }
    }
}
