using System.ComponentModel.DataAnnotations;

namespace HotelReservationApp2.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string RoomNumber { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Range(0, 100000)]
        public int Capacity { get; set; }

        public string ImageUrl { get; set; }
    }
}