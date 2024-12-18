using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly VehiclesContext _context;

        public VehicleController(VehiclesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        // GET: api/VehiclesDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicles(int id)
        {
            var vehicles = await _context.Vehicles.FindAsync(id);

            if (vehicles == null)
            {
                return NotFound();
            }

            return vehicles;
        }

        // PUT: api/VehiclesDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{vin}")]
        public async Task<IActionResult> PutVehicles(string vin, Vehicle vehicles)
        {
            if (vin != vehicles.Vin)
            {
                return BadRequest();
            }

            _context.Entry(vehicles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiclesExists(vin))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.Vehicles.ToListAsync());
        }

        // POST: api/VehiclesDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicles(Vehicle vehicles) 
            // Powinien dostać dto, i w mapperze tworzymy obiekt, nadajemy normalne id
        {
            _context.Vehicles.Add(vehicles);
            await _context.SaveChangesAsync();

            return Ok(await _context.Vehicles.ToListAsync());
        }

        // DELETE: api/VehiclesDetail/5
        [HttpDelete("{vin}")]
        public async Task<IActionResult> DeleteVehicles(string vin)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Vin == vin);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(await _context.Vehicles.ToListAsync()); ;
        }

        private bool VehiclesExists(string vin)
        {
            return _context.Vehicles.Any(e => e.Vin == vin);
        }
    }
}
