
using System.ComponentModel.DataAnnotations;

namespace HotelReservationApp2.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        public Room? Room { get; set; }

        public int NumberOfGuests { get; set; }
    }
}