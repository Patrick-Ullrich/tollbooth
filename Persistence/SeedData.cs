using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TollBoothDBContext(serviceProvider.GetRequiredService<DbContextOptions<TollBoothDBContext>>()))
            {
                if (context.Customers.Any())
                {
                    // Already seeded
                    return;
                }

                context.Customers.AddRange(
                    new Customer
                    {
                        CustomerId = 1,
                        LicensePlate = "ABC123"
                    },
                    new Customer
                    {
                        CustomerId = 2,
                        LicensePlate = "DEF123"
                    },
                    new Customer
                    {
                        CustomerId = 3,
                        LicensePlate = "GHI123"
                    });

                context.SaveChanges();
            }
        }
    }
}
