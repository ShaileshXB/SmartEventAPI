using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEventAPI.Data;
using SmartEventAPI.Models;

namespace SmartEventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL EVENTS
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var data = await _context.Events.ToListAsync();
            return Ok(data);
        }

        // ✅ GET EVENT BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var data = await _context.Events.FindAsync(id);

            if (data == null)
                return NotFound("Event not found");

            return Ok(data);
        }

        // ✅ CREATE EVENT (USE DTO)
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ev = new Event
            {
                EventName = dto.EventName,
                Date = dto.Date,
                Location = dto.Location,
                Description = dto.Description,
                Price = dto.Price,
                Seat = dto.Seat
            };

            await _context.Events.AddAsync(ev);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventById), new { id = ev.EventId }, ev);
        }

        // ✅ UPDATE EVENT (USE DTO)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _context.Events.FindAsync(id);
            if (existing == null)
                return NotFound("Event not found");

            existing.EventName = dto.EventName;
            existing.Date = dto.Date;
            existing.Location = dto.Location;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Seat = dto.Seat;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // ✅ DELETE EVENT
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var data = await _context.Events.FindAsync(id);

            if (data == null)
                return NotFound("Event not found");

            _context.Events.Remove(data);
            await _context.SaveChangesAsync();

            return Ok("Event Deleted Successfully");
        }
    }
}