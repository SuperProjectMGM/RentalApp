﻿using System;
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
    public class VehiclesDetailController : ControllerBase
    {
        private readonly VehiclesContext _context;

        public VehiclesDetailController(VehiclesContext context)
        {
            _context = context;
        }

        // GET: api/VehiclesDetail
        // Bug here
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicles>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        // GET: api/VehiclesDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicles>> GetVehicles(int id)
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
        public async Task<IActionResult> PutVehicles(string vin, Vehicles vehicles)
        {
            if (vin != vehicles.VinId)
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
        public async Task<ActionResult<Vehicles>> PostVehicles(Vehicles vehicles) 
            // Powinien dostać dto, i w mapperze tworzymy obiekt, nadajemy normalne id
        {
            vehicles.VehicleId = Nanoid.Generate(size: 10);
            _context.Vehicles.Add(vehicles);
            await _context.SaveChangesAsync();

            return Ok(await _context.Vehicles.ToListAsync());
        }

        // DELETE: api/VehiclesDetail/5
        [HttpDelete("{vin}")]
        public async Task<IActionResult> DeleteVehicles(string vin)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.VinId == vin);
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
            return _context.Vehicles.Any(e => e.VinId == vin);
        }
    }
}
