using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Vehicles
{
    public class VehicleDTO : IMapFrom<Vehicle>
    {
        public string LicensePlate { get; set; }
        public string CustomerEmail { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, VehicleDTO>()
                .ForMember(d => d.CustomerEmail, opts => opts.MapFrom(s => s.Customer.Email));
        }
    }
}
