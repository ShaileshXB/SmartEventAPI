using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEventAPI.Data;
using SmartEventAPI.Models;

namespace SmartEventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(x => x.Name == registerDTO.Name))
                return BadRequest("Name already exists");

            if (await _context.Users.AnyAsync(x => x.Email == registerDTO.Email))
                return BadRequest("Email already exists");

            var user = new User()
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                Role = "User"
            };

            await _context.Users.AddAsync(user);
            //await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered Successfully",
                user.Id,
                user.Name,
                user.Email,
                user.Role
            });
        }

        // ✅ LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _context.Users.FirstOrDefaultAsync(
                x => x.Email == loginDto.Email &&
                     x.Password == loginDto.Password
            );

            if (data == null)
                return Unauthorized("Invalid credentials");

            return Ok(new
            {
                userId = data.Id,
                name = data.Name,
                role = data.Role
            });
        }
    }
}