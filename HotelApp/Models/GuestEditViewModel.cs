using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public class GuestEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email ID is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile No. is required")]
        [RegularExpression(@"^[1-9]{1}[0-9]{9}$")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Address is required"), StringLength(150)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Arrival date is required")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage = "Departure date is required")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; } = DateTime.UtcNow.AddDays(1);
        [Required(ErrorMessage = "Room Type is required")]
        public string RoomType { get; set; }
        public bool CheckedIn { get; set; }


    }
}
