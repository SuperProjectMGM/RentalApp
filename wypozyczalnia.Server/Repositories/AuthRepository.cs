
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Models;

namespace wypozyczalnia.Server.Repositories;

public class AuthRepository: IAuthInterface
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    public AuthRepository(UserManager<IdentityUser> userManager, IConfiguration configuration, AppDbContext context)
    {
        _context = context;
        _userManager = userManager;
        _configuration = configuration;
    }
    // TODO: Change creating employees, now only identity users are createed
    public async Task<IdentityResult> CreateNewUser(RegisterModel model)
    {
        var user = new IdentityUser 
        {
            UserName = model.Email, Email = model.Email
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        return result;
    }


    public string? GetToken(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT_KEY"]!);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<string?> CheckLogin(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return null;

        var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!passwordValid)
            return null;

        return GetToken(user);
    } 
}