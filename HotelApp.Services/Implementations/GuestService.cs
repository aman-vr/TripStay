using HotelApp.Entity;
using HotelApp.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Services.Implementations
{
    public class GuestService : IGuestService
    {
        private readonly ApplicationDbContext _context;
        public GuestService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Guest newGuest)
        {
            await _context.Guests.AddAsync(newGuest);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int guestId)
        {
            var guest = GetById(guestId);
            _context.Remove(guest);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Guest> GetByEmail(string guestEmail) =>
            _context.Guests.Where(g => g.Email == guestEmail).AsNoTracking().OrderByDescending(g => g.FromDate);

        public IEnumerable<Guest> GetOldRecord() => _context.Guests.Where(g => g.ToDate < DateTime.UtcNow.Date).AsNoTracking().OrderByDescending(g => g.FromDate);
        public IEnumerable<Guest> GetUpcomingBookings() => _context.Guests.Where(g => g.ToDate >= DateTime.UtcNow.Date).AsNoTracking().OrderByDescending(g => g.FromDate);

        public Guest GetById(int guestId) => 
            _context.Guests.Where(g => g.Id == guestId).FirstOrDefault();

        public async Task UpdateAsync(Guest guest)
        {
            _context.Update(guest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int guestId)
        {
            var guest = GetById(guestId);
            _context.Update(guest);
            await _context.SaveChangesAsync();
        }


        public int PerDayCharges(string roomType)
        {
            var perDayCharges = 0;
            if (roomType == "Single King")
                perDayCharges = 1000;
            else if (roomType == "Double Queen")
                perDayCharges = 1800;
            else if (roomType == "Executive Suite")
                perDayCharges = 2500;

            return perDayCharges;
        }
        public decimal GetTotalCharges(DateTime fromDate, DateTime toDate, int perDayCharges)
        {
            int days = (int)(toDate - fromDate).TotalDays;
            return days * perDayCharges;
        }
    }
}
