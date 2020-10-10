using Application.Common.Abstracts;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Vehicles
{
    public class VehiclesService : ServiceBase
    {
        private readonly ITollBoothDBContext _context;
        private readonly IMapper _mapper;

        public VehiclesService(ITollBoothDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VehicleDTO> GetVehicleById(int id)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(v => v.VehicleId == id);

            if (vehicle == null)
            {
                throw new NotFoundException(nameof(Vehicle), id);
            }

            return _mapper.Map<VehicleDTO>(vehicle);
        }
    }
}
