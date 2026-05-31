using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEventAPI.Data;

namespace SmartEventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/Display")]
        public async Task<IActionResult> getAdmin()
        {
            var booking = await _context.Bookings.CountAsync();
            var totalAmount = await _context.Bookings.SumAsync(o => o.TotalAmount);
            var user = await _context.Users.CountAsync();
            var events = await _context.Events.CountAsync();

            var result = new
            {
                booking,
                totalAmount,
                user,
                events
            };

            return Ok(result);
        }
    }
}
