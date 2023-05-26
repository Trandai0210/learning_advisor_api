using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Domain.Models;
using Server.Services.faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListFaculty()
        {
            IEnumerable<Faculty> faculties = await _facultyService.GetList();
            var result = from f in faculties
                         select new
                         {
                             FacultyId = f.FacultyId,
                             Name = f.Name
                         };
            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddFaculty(string name)
        {
            IEnumerable<Faculty> faculties = await _facultyService.GetList();
            bool checkContent = faculties.Any(a => a.Name != name);
            if (name != null && checkContent == true)
            {
                Faculty faculty = new Faculty() { Name = name };
                await _facultyService.Create(faculty);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateFaculty(int facultyID, string name)
        {
            Faculty faculty = await _facultyService.GetById(facultyID);
            faculty.Name = name;
            await _facultyService.Update(faculty);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteFaculty(int id)
        {
            await _facultyService.Delete(id);
            return Ok();
        }
    }
}
