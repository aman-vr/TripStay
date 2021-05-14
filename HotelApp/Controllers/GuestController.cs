using HotelApp.Entity;
using HotelApp.Models;
using HotelApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Controllers
{
    [Authorize]
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;
        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }
        public IActionResult Index(int? pageNumber)
        {
            var guests = _guestService.GetByEmail(User.Identity.Name).Select(guest => new GuestIndexViewModel
            {
                Id = guest.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                MobileNo = guest.MobileNo,
                RoomType = guest.RoomType,
                FromDate = guest.FromDate,
                ToDate = guest.ToDate,
                CheckedIn = guest.CheckedIn,
                TotalCharges = Math.Round(guest.TotalCharges,2),
                Email = guest.Email
            }).ToList();
            int pageSize = 3;
            return View(BookingListPagination<GuestIndexViewModel>.Create(guests,pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new GuestCreateViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GuestCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.FromDate >= DateTime.UtcNow.Date && model.ToDate > DateTime.UtcNow.Date)
                {
                    var guest = new Guest
                    {
                        Id = model.Id,
                        MobileNo = model.MobileNo,
                        Email = model.Email,
                        Address = model.Address,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        RoomType = model.RoomType,
                        FromDate = model.FromDate,
                        ToDate = model.ToDate,
                        TotalCharges = _guestService.GetTotalCharges(model.FromDate, model.ToDate,
                        _guestService.PerDayCharges(model.RoomType))
                    };
                    await _guestService.CreateAsync(guest);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var guest = _guestService.GetById(id);
            
            if (guest == null)
                return NotFound();
            
            var model = new GuestEditViewModel()
            {
                Id = guest.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                MobileNo = guest.MobileNo,
                Email = guest.Email,
                Address = guest.Address,
                RoomType = guest.RoomType,
                FromDate = guest.FromDate,
                ToDate = guest.ToDate
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GuestEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var guest = _guestService.GetById(model.Id);

                if (guest == null)
                    return NotFound();

                guest.MobileNo = model.MobileNo;
                guest.Email = model.Email;
                guest.Address = model.Address;
                guest.RoomType = model.RoomType;
                guest.TotalCharges = _guestService.GetTotalCharges(model.FromDate, model.ToDate,
                        _guestService.PerDayCharges(model.RoomType));
                guest.FromDate = model.FromDate;
                guest.ToDate = model.ToDate;

                await _guestService.UpdateAsync(guest);
                return RedirectToAction(nameof(Index));
            }
            return View();
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
                RoomType = guest.RoomType
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var guest = _guestService.GetById(id);
            if (guest == null)
                return NotFound();
            var model = new GuestDeleteViewModel()
            {
                Id = guest.Id
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(GuestDeleteViewModel model)
        {
            await _guestService.Delete(model.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
