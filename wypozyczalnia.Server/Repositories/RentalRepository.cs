using Microsoft.EntityFrameworkCore;
using wypozyczalnia.Server.BrowserProviders;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Messages;
using wypozyczalnia.Server.Models;
using wypozyczalnia.Server.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace wypozyczalnia.Server.Repositories;

public class RentalRepository : IRentalInterface
{
    private readonly RabbitMessageService _messageService;
    private readonly IAuthInterface _authRepository;
    private readonly AppDbContext _context;
    public RentalRepository(AppDbContext context, RabbitMessageService messageService, IAuthInterface authRepo)
    {
        _messageService = messageService;
        _context = context;
        _authRepository = authRepo;
    }
    
    public async Task StoreRental(Confirmed mess)
    {
        var clientPersonalNumber = mess.PersonalNumber;
        var clientInfo = await _context.ClientInfos.FirstOrDefaultAsync(x => x.PersonalNumber == clientPersonalNumber);
        
        if (clientInfo != null)
        {
           var rental = mess.ToRentalClientExists(clientInfo);
           await _context.Rentals.AddAsync(rental);
        }
        else
        {
            var rental = mess.ToRental();
            await _context.Rentals.AddAsync(rental);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RentToReturn(UserReturn mess)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.Slug == mess.Slug);
        if (rental == null)
            throw new Exception("Client not found in DB");
        rental.Status = RentalStatus.WaitingForReturnAcceptance;
        rental.ReturnLongtitude = mess.Longtitude;
        rental.ReturnLatitude = mess.Latitude;
        rental.ReturnClientDescription = mess.Description;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Rental>> GetRentalsToReturnAcceptance()
    {
        List<Rental> ret = new List<Rental>();
        var tmp = await _context.Rentals.Include(r => r.UserInfo).ToListAsync();
        foreach (var rent in tmp)
        {
            if (rent.Status == RentalStatus.WaitingForReturnAcceptance)
                ret.Add(rent);
        }

        return ret;
    }

    public async Task<bool> AcceptReturnOfRental(int rentId)
    {
        var rent = await _context.Rentals.FirstOrDefaultAsync(x => x.RentalId == rentId);
        if (rent == null)
            return false;

        var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Vin == rent.Vin);
        if (vehicle == null)
            return false;
        
        rent.Status = RentalStatus.Returned;
        await _context.SaveChangesAsync();

        var provider = BrowserAdapterFactory.CreateBrowser(rent.BrowserProviderIdentifier, _messageService);
        try
        {
            await provider.AcceptReturn(rent, vehicle);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while accepting return: {ex.Message}");
        }

        return true;
    }
    
    public async Task<List<Rental>> GetPendingRentals()
    {
        List<Rental> ret = new List<Rental>();
        var tmp = await _context.Rentals.Include(r => r.UserInfo).ToListAsync();
        foreach (var rent in tmp)
        {
            if (rent.Status == RentalStatus.Confirmed)
                ret.Add(rent);
        }

        return ret;
    }

    public async Task<bool> AcceptRental(int rentalId)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.RentalId == rentalId);
        if (rental is null)
            return false;
        rental.Status = RentalStatus.Completed;
        await _context.SaveChangesAsync();
        
        var browser = BrowserAdapterFactory.CreateBrowser(rental.BrowserProviderIdentifier, _messageService);
        try
        {
            await browser.RentalCompleted(rental);
        }
        catch (Exception ex)
        {
            throw new Exception($"Rental acceptance failed: {ex.Message}");
        }

        return true;
    }

    public async Task<bool> AddPhotoToRental(int rentId, string photoUrl)
    {
        var rent = await _context.Rentals.FirstOrDefaultAsync(rental => rental.RentalId == rentId);
        if (rent == null)
            return false;
        
        rent.PhotoUrl = photoUrl;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddEmployeeInfos(int rentId, string token, string description)
    {
        var employeeId = _authRepository.ReturnIdFromToken(token);
        if (employeeId is null)
        {
            return false;
        }

        var rent = await _context.Rentals.FirstOrDefaultAsync(rental => rental.RentalId == rentId);
        if (rent == null)
            return false;
        rent.ReturnEmployeeDescription = description;
        rent.EmployeeId = employeeId;
        await _context.SaveChangesAsync();
        return true;
    }
}