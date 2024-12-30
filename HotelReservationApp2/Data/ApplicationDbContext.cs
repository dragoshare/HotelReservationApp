using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelReservationApp2.Models;

namespace HotelReservationApp2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            return !Reservations.Any(r => r.RoomId == roomId &&
                                          ((r.CheckInDate <= checkOutDate && r.CheckOutDate >= checkInDate)));
        }

        // Tabele aplikacyjne
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Miejsce na ewentualne zmiany nazw tabel Identity itd.
        }
    }
}