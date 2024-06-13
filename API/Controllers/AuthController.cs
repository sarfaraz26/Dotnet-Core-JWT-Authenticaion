using API.Data;
using API.DTO;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bcrypt = BCrypt.Net.BCrypt;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly AppDbContext _dbContext;
        public AuthController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> SignUp(User dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Please provide proper arguments");
                }
                else
                {
                    dto.Password = Bcrypt.HashPassword(dto.Password);
                    _dbContext.Users.Add(dto);
                    await _dbContext.SaveChangesAsync();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            try
            {
                var user = AuthenticateUser(dto);

                if (user != null)
                {
                    var token = GenerateToken(user);
                    return Ok(new {token});
                }
                return NotFound("User not found");
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
        }


        #region Helper methods
        private User AuthenticateUser(LoginDTO dto)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Username == dto.Username);

            if (currentUser == null)
                return null;
            else
                return Bcrypt.Verify(dto.Password, currentUser.Password) ? currentUser : null;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],_configuration["Jwt:Audience"],claims,expires: DateTime.Now.AddMinutes(15),signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

    }
}
