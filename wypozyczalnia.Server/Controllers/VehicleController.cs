﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleInterface _vehicleRepository;
        public VehicleController(IVehicleInterface vehicleInterface)
        {
            _vehicleRepository = vehicleInterface;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehicles()
        {
            return Ok(await _vehicleRepository.ReturnVehicles());
        }

        [HttpGet("{vin}")]
        public async Task<ActionResult<VehicleDTO>> GetVehicles(string vin)
        {
            var vehDTO = await _vehicleRepository.FindVehicle(vin); 
            return Ok(vehDTO);
        }

        [HttpPut("{vin}")]
        public async Task<IActionResult> PutVehicle(string vin, [FromBody] VehicleDTO dto)
        {
            if (await _vehicleRepository.ChangeVehicle(vin, dto))
                return Ok();
            else
            {
                return NotFound("There is no vehicle with such vin");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostVehicles([FromBody] VehicleDTO vehicleDTO) 
        {
            if (await _vehicleRepository.AddVehicle(vehicleDTO))
                return Ok();
            return BadRequest("Can't add new Vehicle"); 
        }

        [HttpDelete("{vin}")]
        public async Task<IActionResult> DeleteVehicle(string vin)
        {
            if (await _vehicleRepository.DeleteVehicle(vin))
                return Ok();
            return NotFound("There is no vehicle with such vin");
        }


        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetAvailableVehicles(
            [FromQuery] DateTime start,
            [FromQuery] DateTime end)
        {
            return Ok(await _vehicleRepository.ReturnVehicles(start, end));
        }

        [HttpGet("rented")]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetCarsWithActiveRents()
        {
            return Ok(await _vehicleRepository.ReturnCurrentlyRentedVehicles());
        }

        [HttpGet("rentalsForCar")]
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetRentalsForCar([FromQuery] string vin)
        {
            return Ok(await _vehicleRepository.ReturnAllRentalsForVehicle(vin));
        }
    }
}