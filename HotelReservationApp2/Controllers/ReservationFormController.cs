using HotelReservationApp2.Data;
using HotelReservationApp2.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationApp2.Controllers
{
    public class ReservationFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IActionResult Success()
        {
            return View();
        }


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
            if (checkOutDate <= checkInDate)
            {
                ModelState.AddModelError("", "Data wymeldowania musi być późniejsza niż data zameldowania.");
            }

            var room = _context.Rooms.Find(roomId);
            if (room == null)
            {
                ModelState.AddModelError("", "Wybrany pokój nie istnieje.");
            }
            else if (numberOfGuests > room.Capacity)
            {
                ModelState.AddModelError("", $"Wybrany pokój może pomieścić maksymalnie {room.Capacity} osób.");
            }
            else if (!_context.IsRoomAvailable(roomId, checkInDate, checkOutDate))
            {
                ModelState.AddModelError("", "Wybrany pokój nie jest dostępny w podanym terminie.");
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
                    NumberOfGuests = numberOfGuests
                };

                _context.Reservations.Add(reservation);
                _context.SaveChanges();

                return RedirectToAction("Success", new { id = reservation.Id });
            }

            ViewBag.Rooms = _context.Rooms.ToList();
            return View();
        }



    }
}
