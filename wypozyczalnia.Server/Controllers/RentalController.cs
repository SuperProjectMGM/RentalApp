using Microsoft.AspNetCore.Mvc;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalController : ControllerBase
{
    private readonly IRentalInterface _rentalRepo;
    private readonly IMessageHandlerInterface _messageHandler;
    public RentalController(IRentalInterface rentalRepo, IMessageHandlerInterface messageHandler)
    {
        _rentalRepo = rentalRepo;
        _messageHandler = messageHandler;
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
    [FromQuery] string photoUrl)
    {
        var succeed = await _rentalRepo.AddPhotoToRental(rentalId, photoUrl);
        if (!succeed)
            return BadRequest("Can't add photo!");
        succeed = await _rentalRepo.AcceptReturnOfRental(rentalId);
        if (!succeed)
            return BadRequest("Something went wrong in accepting a rental");
        return Ok(new { message = "Rental return accepted." });
    }

    [HttpPost("external-api-rent-offer")]
    public async Task<IActionResult> ExternalApiOffer([FromBody] ConfirmedExternal dto)
    {
        try
        {
            await _messageHandler.ProcessExternalConfirm(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while processing external confirmation message: {e.Message}");
        }

        return Ok();
    }

    [HttpPost("external-api-rental-return")]
    public async Task<IActionResult> ExternalApiReturn([FromBody] UserReturnExternal dto)
    {
        try
        {
            await _messageHandler.ProcessExternalReturn(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while processing external return message: {e.Message}");
        }

        return Ok();
    }
}