using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Domain.Models;
using Server.Services.permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListPermission()
        {
            IEnumerable<Permission> permissions = await _permissionService.GetList();
            var result = from p in permissions
                         select new
                         {
                             PermissionId = p.PermissionId,
                             PermissionName = p.PermissionName
                         };
            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddPermission(string permissionName)
        {
            IEnumerable<Permission> permissions = await _permissionService.GetList();
            bool checkContent = permissions.Any(a => a.PermissionName != permissionName);
            if (permissionName != null && checkContent == true)
            {
                Permission permission = new Permission() { PermissionName = permissionName };
                await _permissionService.Create(permission);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdatePermission(int permissionId, string permissionName)
        {
            IEnumerable<Permission> permissions = await _permissionService.GetList();
            bool checkContent = permissions.Any(a => a.PermissionName != permissionName);
            if (permissionName != null && checkContent == true)
            {
                Permission permission = await _permissionService.GetById(permissionId);
                permission.PermissionName = permissionName;
                await _permissionService.Update(permission);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            await _permissionService.Delete(id);
            return Ok();
        }
    }
}
