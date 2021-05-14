using HotelApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Services
{
    public interface IGuestService
    {
        Task CreateAsync(Guest newGuest);
        Guest GetById(int guestId);
        Task UpdateAsync(Guest guest);
        Task UpdateAsync(int guestId);
        Task Delete(int guestId);
        IEnumerable<Guest> GetByEmail(string guestEmail);
        int PerDayCharges(string roomType);
        decimal GetTotalCharges(DateTime fromDate, DateTime toDate, int perDayCharges);
        IEnumerable<Guest> GetOldRecord();
        IEnumerable<Guest> GetUpcomingBookings();
    }
}
