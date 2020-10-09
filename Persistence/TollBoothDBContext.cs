using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence
{
    public class TollBoothDBContext : DbContext, ITollBoothDBContext
    {
        public TollBoothDBContext(DbContextOptions<TollBoothDBContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
    }
}
