using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalController : ControllerBase
{
    private readonly IRentalInterface _rentalRepo;
    public RentalController(IRentalInterface rentalRepo)
    {
        _rentalRepo = rentalRepo;
    }

    [HttpGet("pending-rentals")]
    public async Task<IActionResult> GetPendingRentals()
    {
        var rentals = await _rentalRepo.GetPendingRentals();
        return Ok(rentals.Select((rent) => rent.ToDto()));
    }

    [HttpPut("accept-rental/{rentalId}")]
    public async Task<IActionResult> AcceptRentalAndSendBack([FromRoute] int rentalId)
    {
        var succeed = await _rentalRepo.AcceptRental(rentalId);
        if (!succeed)
            return BadRequest("Something went wrong");
        return Ok(new { message = "Rental completed successfully. Message has been sent." });
    }

    [HttpGet("pending-rentals-to-return")]
    public async Task<IActionResult> GetPendingRentalsToReturn()
    {
        var rentals = await _rentalRepo.GetRentalsToReturnAcceptance();
        return Ok(rentals.Select((rent) => rent.ToDto()));
    }

    [HttpPut("accept-pending-rental-to-return/{rentalId}")]
    public async Task<IActionResult> AcceptPendingRentalToReturn([FromRoute] int rentalId,
    [FromQuery] string photoUrl, [FromQuery] string employeeDescription, [FromQuery] string token)
    {
        var succeed = await _rentalRepo.AddEmployeeInfos(rentalId, token, employeeDescription);
        succeed = succeed && await _rentalRepo.AddPhotoToRental(rentalId, photoUrl);
        if (!succeed)
            return BadRequest("Can't add photo!");
        succeed = await _rentalRepo.AcceptReturnOfRental(rentalId);
        if (!succeed)
            return BadRequest("Something went wrong in accepting a rental");
        return Ok(new { message = "Rental return accepted." });
    }
}