using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Vehicles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Middleware;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehiclesService _vehicleService;

        public VehiclesController(VehiclesService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorMessage), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VehicleDTO>> Get(int id)
    {
            var vehicle = await _vehicleService.GetVehicleById(id);
            return Ok(vehicle);
    }
}
}
