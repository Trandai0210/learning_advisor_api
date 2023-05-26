using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Domain.Models;
using Server.Services.permission;
using Server.Services.user;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public AuthController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        [HttpPost]
        [Route("GetToken")]
        public async Task<IActionResult> getToken(string email)
        {
            if(email != null)
            {
                IEnumerable<User> users = await _userService.GetList();
                User user = users.First(u=>u.Gmail == email);
                if(user == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = "Lấy token thất bại" });
                } 
                else
                {
                    Permission permission = await _permissionService.GetById((int)user.PermissionId);
                    //var claims = new []
                    //{
                    //    new Claim("role", permission.PermissionName),
                    //};
                    var claims = new[] {
                        new Claim("role", permission.PermissionName),
                        new Claim("userid", user.UserId.ToString()),
                        new Claim("name", user.Name),
                        new Claim("image", user.Image == null ? "https://th.bing.com/th/id/OIP.HHVUf3TYqncgpJXyCMmxyAHaHa?pid=ImgDet&rs=1" : user.Image),
                        new Claim("status", user.Status.ToString()),
                    };
                    var accessToken = GenerateJSONWebToken(claims);
                    SetJWTCookie(accessToken);
                    return Ok(accessToken);
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = "Lấy token thất bại" });
        }

        private string GenerateJSONWebToken(Claim[] claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Tranvudai9876543210"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44302",
                audience: "https://localhost:44302",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials,
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void SetJWTCookie(string token)
        {
            var cookieOptions = new CookieOptions { HttpOnly = true, Expires = DateTime.UtcNow.AddHours(3) };
            Response.Cookies.Append("JwtCookie", token, cookieOptions);
        }
    }
}
