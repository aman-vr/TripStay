using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public class GuestIndexViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string RoomType { get; set; }
        public decimal TotalCharges { get; set; }
        public bool CheckedIn { get; set; }
    }
}
