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
            try
            {
                // 유효성 검사
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
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Server Exception - register"));
            }
        }
        #endregion

        #region 로그인
        // POST: api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            try
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
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Server Exception - login"));
            }
        }
        #endregion

        #region 회원탈퇴
        // DELETE: api/user/delete
        [HttpPost("deleteid")]
        public async Task<IActionResult> Delete([FromBody] User login)
        {
            try
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

                    // 사용자 삭제
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();

                    return Ok(new ApiResponse(200, "User deleted successfully."));
                }
                return BadRequest(new ApiResponse(400, "Invalid user data."));
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Server Exception - deleteid"));
            }
        }
        #endregion

        #region 비밀번호 변경
        // POST: api/user/changepassword
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] User_ChangePassword model)
        {
            try
            {
                // 유효성 검사
                if (ModelState.IsValid)
                {
                    var user = await _context.Users
                        .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.OldPassword);

                    if (user == null)
                    {
                        return Unauthorized(new ApiResponse(401, "Invalid username or password."));
                    }

                    if (model.OldPassword == model.NewPassword)
                    {
                        return BadRequest(new ApiResponse(400, "New password cannot be the same as the old password."));
                    }

                    // 비밀번호 변경
                    user.Password = model.NewPassword;
                    await _context.SaveChangesAsync();

                    return Ok(new ApiResponse(200, "Password changed successfully."));
                }

                return BadRequest(new ApiResponse(400, "Invalid data."));
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, "Server Exception - changepassword"));
            }
        }
        #endregion
    }
}