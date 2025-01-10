using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using wypozyczalnia.Server.Models;

public class Employee
{
    public int Id { get; set; }
    public IdentityUser IdentityUser { get; set; } = new IdentityUser();
    public List<Rental> Rentals { get; set; } = new List<Rental>();
}