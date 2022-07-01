using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KaseyWebApi.Context;
using KaseyWebApi.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KaseyWebApi.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class TokenController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public TokenController(IConfiguration config, ApplicationDbContext context)
    {
        _configuration = config;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserInfo userData)
    {
        if (userData != null && userData.Email != null && userData.Password != null)
        {
            var user = await GetUser(userData.Email, userData.Password);

            if (user != null)
            {
                //create claims details based on the user information
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("DisplayName", user.DisplayName),
                    new Claim("UserName", user.UserName),
                    new Claim("Email", user.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMonths(6),
                    signingCredentials: signIn);

                return Ok(new { encryptedApiKey = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return BadRequest(new { message = "Invalid credentials" });
        }

        return BadRequest();
    }

    private async Task<UserInfo> GetUser(string email, string password)
    {
        return await Task.FromResult(
            await _context
                .Users
                .FirstOrDefaultAsync(u =>
                    u.Email == email && u.Password == password));
    }
}