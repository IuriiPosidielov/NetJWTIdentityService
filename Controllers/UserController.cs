using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using service.Models;

//using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly PuzzleContext _dbcontext;
        private IConfiguration _jwtSettings;

        public UserController(PuzzleContext dbcontext, IConfiguration config)
        {
            _dbcontext = dbcontext;
            _jwtSettings = config;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate
            ([FromBody]UserCredentials userCredentials)
        {
            if (userCredentials == null)
            {
                return BadRequest("no credentials");
            }

            var user = await _dbcontext.UserCredentials
                .FirstOrDefaultAsync
                    (u => u.UserName == userCredentials.UserName && u.Password==userCredentials.Password);

            if (user == null || userCredentials == null)
                return Unauthorized();
            
            var jwttokenhandler =new JwtSecurityTokenHandler();
            
            //var jwttokendescr =new securitytokentdescriptor()
            //create claims details based on the user information
            string userName = userCredentials.UserName ?? "";

            var claims = new[] {
                //new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", userCredentials.ID.ToString()),
                new Claim("DisplayName", userName),
                new Claim("UserName", userName),
                //new Claim("Email", userCredentials.UserName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _jwtSettings["JwtSettings:SecurityKey"] ?? ""));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _jwtSettings["JwtSettings:Issuer"],
                _jwtSettings["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));    
        } 
    }
}
