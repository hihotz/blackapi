using blackapi.Data;
using blackapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blackapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region 회원가입
        // POST: api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == user.Username);

                if (existingUser != null)
                {
                    return BadRequest(new ApiResponse(400, "Username already exists."));
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse(200, "User registered successfully."));
            }

            return BadRequest(new ApiResponse(400, "Invalid user data."));
        }
        #endregion

        #region 로그인
        // POST: api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            // 유효성 검사
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == login.Username && u.Password == login.Password);

                if (user == null)
                {
                    return Unauthorized(new ApiResponse(401, "Invalid username or password."));
                }

                return Ok(new ApiResponse(200, "Login successful."));
            }

            return BadRequest(new ApiResponse(400, "Invalid login data."));
        }
        #endregion
    }
}