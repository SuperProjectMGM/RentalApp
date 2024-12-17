
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

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Uri>> GetSasUri()
    {
        var uri = await _storageRepo.GetUriToStorage();
        return Ok(uri);
    }

}