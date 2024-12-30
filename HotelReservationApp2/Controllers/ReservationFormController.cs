using HotelReservationApp2.Data;
using HotelReservationApp2.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationApp2.Controllers
{
    public class ReservationFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Rooms = _context.Rooms.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int roomId, string fullName, string email, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests)
        {
            var room = _context.Rooms.Find(roomId);
            if (room == null)
            {
                ModelState.AddModelError("", "Selected room does not exist.");
            }
            else if (numberOfGuests > room.Capacity)
            {
                ModelState.AddModelError("", $"The selected room can accommodate up to {room.Capacity} guests.");
            }
            else if (!_context.IsRoomAvailable(roomId, checkInDate, checkOutDate))
            {
                ModelState.AddModelError("", "The selected room is not available for the chosen dates.");
            }

            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    RoomId = roomId,
                    FullName = fullName,
                    Email = email,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    NumberOfGuests = numberOfGuests // Zakładamy, że Reservation ma to pole
                };

                _context.Reservations.Add(reservation);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Rooms = _context.Rooms.ToList();
            return View();
        }

    }
}
