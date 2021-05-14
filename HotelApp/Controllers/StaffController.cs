using HotelApp.Models;
using HotelApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RotativaCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Controllers
{
    [Authorize(Roles ="Manager,Staff")]
    public class StaffController : Controller
    {
        private readonly IGuestService _guestService;

        public StaffController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        public IActionResult Index(int? pageNumber)
        {
            var guests = _guestService.GetUpcomingBookings().Select(guest => new GuestIndexViewModel
            {
                Id = guest.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                MobileNo = guest.MobileNo,
                RoomType = guest.RoomType,
                FromDate = guest.FromDate,
                ToDate = guest.ToDate,
                TotalCharges = Math.Round(guest.TotalCharges, 2)
            }).ToList();
            int pageSize = 3;
            return View(BookingListPagination<GuestIndexViewModel>.Create(guests, pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var guest = _guestService.GetById(id);
            if (guest == null)
                return NotFound();
            var model = new GuestDetailViewModel()
            {
                Id = guest.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                MobileNo = guest.MobileNo,
                Email = guest.Email,
                Address = guest.Address,
                TotalCharges = guest.TotalCharges,
                FromDate = guest.FromDate.Date,
                ToDate = guest.ToDate.Date,
                RoomType = guest.RoomType,
            };
            return View(model);
        }
        [Authorize(Roles = "Manager")]
        public IActionResult Old(int? pageNumber)
        {
            var guests = _guestService.GetOldRecord().Select(guest => new GuestIndexViewModel
            {
                Id = guest.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                MobileNo = guest.MobileNo,
                RoomType = guest.RoomType,
                FromDate = guest.FromDate,
                ToDate = guest.ToDate,
                TotalCharges = Math.Round(guest.TotalCharges, 2)
            }).ToList();
            int pageSize = 3;
            return View(BookingListPagination<GuestIndexViewModel>.Create(guests, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Receipt(int id)
        {
            var guest = _guestService.GetById(id);
            if (guest == null)
            {
                return NotFound();
            }
            var model = new GuestDetailViewModel()
            {
                Id = guest.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                MobileNo = guest.MobileNo,
                Email = guest.Email,
                Address = guest.Address,
                TotalCharges = guest.TotalCharges,
                FromDate = guest.FromDate.Date,
                ToDate = guest.ToDate.Date,
                RoomType = guest.RoomType

            };
            return View(model);
        }

        public IActionResult PrintReceipt(int id)
        {
            var receipt = new ActionAsPdf("Receipt", new { id })
            {
                FileName = "receipt.pdf"
            };
            return receipt;
        }



    }
}
