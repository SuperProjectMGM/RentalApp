
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wypozyczalnia.Server.Interfaces;

namespace wypozyczalnia.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StorageController: ControllerBase
{
    private IStorageInterface _storageRepo;
    public StorageController(IStorageInterface storageInterface)
    {
        _storageRepo = storageInterface;
    }

    [HttpGet("vehicles")]
    [Authorize]
    public async Task<ActionResult<Uri>> GetSasUriVehicles()
    {
        // It's hardcoded
        var uri = await _storageRepo.GetUriToStorage("vehicles");
        return Ok(uri);
    }

    
    [HttpGet("rentals")]
    [Authorize]
    public async Task<ActionResult<Uri>> GetSasUriRentlas()
    {
        var uri = await _storageRepo.GetUriToStorage("rentals");
        return Ok(uri);
    }
}