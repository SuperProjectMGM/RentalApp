using Microsoft.AspNetCore.Identity;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Interfaces;

public interface IAuthInterface
{
    public Task<IdentityResult> CreateNewUser(RegisterModel model);
    public string? GetToken(IdentityUser user);
    public Task<string?> CheckLogin(LoginModel model);
}