using Application.Common.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            var assembly = typeof(DependencyInjection).Assembly;
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(ServiceBase)))
                {
                    services.AddTransient(type);
                }
            }

            return services;
        }
    }
}
