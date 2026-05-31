using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEventAPI.Data;
using SmartEventAPI.Models;

namespace SmartEventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ CREATE BOOKING
        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ev = await _context.Events.FindAsync(dto.EventId);
            if (ev == null)
                return NotFound("Event not found");

            // ✅ CHECK SEAT AVAILABILITY
            if (dto.Seats > ev.Seat)
                return BadRequest("Not enough seats available");

            // ✅ CALCULATE TOTAL
            var totalAmount = ev.Price * dto.Seats;

            var booking = new Booking()
            {
                EventId = dto.EventId,
                UserId = dto.UserId,
                Seats = dto.Seats,
                TotalAmount = totalAmount
            };

            // ✅ REDUCE SEATS
            ev.Seat -= dto.Seats;

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Booking Successful",
                BookingId = booking.BookingId,
                EventName = ev.EventName,
                Seats = dto.Seats,
                TotalAmount = totalAmount
            });
        }

        // ✅ GET BOOKINGS BY USER
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBookings(int userId)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Event)
                .Where(b => b.UserId == userId)
                .Select(b => new
                {
                    b.BookingId,
                    b.Seats,
                    b.TotalAmount,
                    EventName = b.Event!.EventName,
                    EventDate = b.Event.Date,
                    Location = b.Event.Location
                })
                .ToListAsync();

            if (bookings.Count == 0)
                return NotFound("No bookings found");

            return Ok(bookings);
        }

        // ✅ CANCEL BOOKING
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Event)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
                return NotFound("Booking not found");

            // ✅ RETURN SEATS BACK
            booking.Event!.Seat += booking.Seats;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Booking Cancelled",
                BookingId = booking.BookingId
            });
        }
    }
}