using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MileageTrackerAPI.Data;
using MileageTrackerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MileageTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly MileageTrackerContext _context;

        public VehiclesController(MileageTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<List<Vehicle>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        // POST: api/Vehicles
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);  // Corrected this line to properly reference the Vehicle class
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicles", new { id = vehicle.Id }, vehicle);
        }
    }
}