using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence
{
    public class TollBoothDBContext : DbContext
    {
        public TollBoothDBContext(DbContextOptions<TollBoothDBContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}
