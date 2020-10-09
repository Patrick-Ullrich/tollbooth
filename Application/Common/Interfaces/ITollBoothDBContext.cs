using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITollBoothDBContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<ApiKey> ApiKeys { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
