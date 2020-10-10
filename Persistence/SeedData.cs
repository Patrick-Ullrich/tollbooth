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
                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(
                        new Customer
                        {
                            CustomerId = 1,
                            Password = "password1",
                            Email = "customer1@tollbooth.xyz"
                        },
                        new Customer
                        {
                            CustomerId = 2,
                            Password = "password2",
                            Email = "customer2@tollbooth.xyz"
                        },
                        new Customer
                        {
                            CustomerId = 3,
                            Password = "password3",
                            Email = "customer3@tollbooth.xyz"
                        }
                    );
                }

                if (!context.ApiKeys.Any())
                {
                    context.ApiKeys.Add(
                        new ApiKey
                        {
                            ApiKeyId = 1,
                            Key = "f0e99ca6-b79d-4c9c-ab43-5839c323ea6d",
                            Name = "Government"
                        }
                    );
                }

                if(!context.Vehicles.Any())
                {
                    context.Vehicles.AddRange(
                        new Vehicle
                        {
                            VehicleId = 1,
                            CustomerId = 1,
                            LicensePlate = "A1A 1A1"
                        },
                        new Vehicle
                        {
                            VehicleId = 2,
                            CustomerId = 1,
                            LicensePlate = "B1B 1B1"
                        },
                        new Vehicle
                        {
                            VehicleId = 3,
                            CustomerId = 2,
                            LicensePlate = "A2A 2A2"
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
