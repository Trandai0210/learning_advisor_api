using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Domain.Models;
using Server.Services.permission;
using Server.Services.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public UserController(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListUser()
        {
            IEnumerable<User> users = await _userService.GetList();
            IEnumerable<Permission> permissions = await _permissionService.GetList();
            var result = from u in users
                         join p in permissions on u.PermissionId equals p.PermissionId
                         select new
                         {
                             UserId = u.UserId,
                             Name = u.Name,
                             Gmail = u.Gmail,
                             Phone = u.Phone,
                             Image = u.Image == null ? "https://th.bing.com/th/id/OIP.HHVUf3TYqncgpJXyCMmxyAHaHa?pid=ImgDet&rs=1" : u.Image,
                             Dob = u.Dob,
                             Status = u.Status,
                             PermissionId = p.PermissionId,
                             PermissionName = p.PermissionName
                         };
            return Ok(result);
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddUser(string name, string gmail, string phone, string image, string dob)
        {
            IEnumerable<User> users = await _userService.GetList();
            int count = users.Where(u => u.Gmail == gmail).Count();
            if(count == 0)
            {
                DateTime? dayOfBirth = DateTime.Parse(dob);
                User user = new User() { Name = name, Gmail = gmail, Phone = phone, Image = image, Dob = dayOfBirth, Status = true };
                await _userService.Create(user);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Block/{id}")]
        public async Task<IActionResult> Block(int id)
        {
            User user = await _userService.GetById(id);
            user.Status = false;
            await _userService.Update(user);
            return Ok();
        }

        [HttpPost]
        [Route("UnBlock/{id}")]
        public async Task<IActionResult> UnBlock(int id)
        {
            User user = await _userService.GetById(id);
            user.Status = true;
            await _userService.Update(user);
            return Ok();
        }

        [HttpPost]
        [Route("SetPermission")]
        public async Task<IActionResult> SetPermission(int userId, int permissionId)
        {
            User user = await _userService.GetById(userId);
            user.Status = true;
            user.PermissionId = permissionId;
            await _userService.Update(user);
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser(int id, string name, string gmail, string phone, string image, string dob)
        {
            DateTime? dayOfBirth = DateTime.Parse(dob);
            User user = await _userService.GetById(id);
            user.Name = name;
            user.Gmail = gmail;
            user.Phone = phone;
            user.Image = image;
            user.Dob = dayOfBirth;
            await _userService.Update(user);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateImage")]
        public async Task<IActionResult> UpdateImage(int id, string image)
        {
            User user = await _userService.GetById(id);
            user.Image = image;
            await _userService.Update(user);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> getUserById(int id)
        {
            User user = await _userService.GetById(id);
            if(user == null)
            {
                return BadRequest();
            }
            Permission permission = await _permissionService.GetById(user.PermissionId == null ? 1 : 2);
            var result = new
            {
                UserId = user.UserId,
                Name = user.Name,
                Gmail = user.Gmail,
                Phone = user.Phone,
                Image = user.Image == null ? "https://th.bing.com/th/id/OIP.HHVUf3TYqncgpJXyCMmxyAHaHa?pid=ImgDet&rs=1" : user.Image,
                Dob = user.Dob,
                Status = user.Status,
                PermissionId = user.PermissionId,
                PermissionName = permission.PermissionName
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("GetByEmail")]
        public async Task<IActionResult> getUserByEmail(String email)
        {
            if(email == null)
            {
                return BadRequest();
            }
            IEnumerable<User> users = await _userService.GetList();
            User user = users.First(u => u.Gmail == email);
            Permission permission = await _permissionService.GetById(user.PermissionId == null ? 1 : 2);
            var result = new
            {
                UserId = user.UserId,
                Name = user.Name,
                Gmail = user.Gmail,
                Phone = user.Phone,
                Image = user.Image == null ? "https://th.bing.com/th/id/OIP.HHVUf3TYqncgpJXyCMmxyAHaHa?pid=ImgDet&rs=1" : user.Image,
                Dob = user.Dob,
                Status = user.Status,
                PermissionId = user.PermissionId,
                PermissionName = permission.PermissionName
            };
            return Ok(result);
        }
    }
}
