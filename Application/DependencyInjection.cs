using Application.Common.Abstracts;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
