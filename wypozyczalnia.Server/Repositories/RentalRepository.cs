using Microsoft.EntityFrameworkCore;
using wypozyczalnia.Server.BrowserProviders;
using wypozyczalnia.Server.DTOs;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Models;
using wypozyczalnia.Server.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace wypozyczalnia.Server.Repositories;

public class RentalRepository : IRentalInterface
{
    private readonly RabbitMessageService _messageService;
    private readonly AppDbContext _context;
    public RentalRepository(AppDbContext context, RabbitMessageService messageService)
    {
        _messageService = messageService;
        _context = context;
    }
    
    public async Task StoreRental(MessageMgmConfirmed mess)
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

    // public async Task RentToReturn(MessageMgmConfirmed mess)
    // {
    //     var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.Slug == mess.Slug);
    //     if (rental == null)
    //         throw new Exception("Client not found in DB");
    //     // change status
    //     rental.Status = RentalStatus.WaitingForReturnAcceptance;
    //     await _context.SaveChangesAsync();
    // }

    public async Task<List<Rental>> GetRentalsToReturnAcceptance()
    {
        List<Rental> ret = new List<Rental>();
        // This include is important, why?
        var tmp = await _context.Rentals.Include(r => r.UserInfo).ToListAsync();
        foreach (var rent in tmp)
        {
            if (rent.Status == RentalStatus.WaitingForReturnAcceptance)
                ret.Add(rent);
        }

        return ret;
    }

    // public async Task<bool> AcceptReturnOfRental(int rentId)
    // {
    //     var rent = await _context.Rentals.FirstOrDefaultAsync(x => x.RentalId == rentId);
    //     if (rent == null)
    //         return false;
    //
    //     rent.Status = RentalStatus.Returned;
    //     await _context.SaveChangesAsync();
    //
    //     return await SendRentalReturnAcceptedMessage(rent);
    // }
    
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
}