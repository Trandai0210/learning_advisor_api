using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Domain.Models;
using Server.Services.faculty;
using Server.Services.message;
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
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IFacultyService _facultyService;
        private readonly IUserService _userService;
        public MessageController(IMessageService messageService, IFacultyService facultyService, IUserService userService)
        {
            _messageService = messageService;
            _facultyService = facultyService;
            _userService = userService;
        }
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListMessage()
        {
            IEnumerable<Message> messages = await _messageService.GetList();
            IEnumerable<Faculty> faculties = await _facultyService.GetList();
            IEnumerable<User> users = await _userService.GetList();
            var result = from m in messages
                         join f in faculties on m.FacultyId equals f.FacultyId
                         join u in users on m.UserId equals u.UserId
                         orderby m.CreatedAt descending
                         select new
                         {
                             MessageId = m.MessageId,
                             Content = m.Content,
                             CreatedAt = m.CreatedAt,
                             UserId = m.UserId,
                             UserName = u.Name,
                             Gmail = u.Gmail,
                             UserImage = u.Image == null ? "https://th.bing.com/th/id/OIP.HHVUf3TYqncgpJXyCMmxyAHaHa?pid=ImgDet&rs=1" : u.Image,
                             FacultyId = m.FacultyId,
                             FacultyName = f.Name
                         };
            return Ok(result);
        }

        [HttpGet]
        [Route("ListByFacultyId")]
        public async Task<IActionResult> ListMessageByFacultyId(int id)
        {
            IEnumerable<Message> messages = await _messageService.GetList();
            messages = messages.Where(m => m.FacultyId == id);
            IEnumerable<Faculty> faculties = await _facultyService.GetList();
            IEnumerable<User> users = await _userService.GetList();
            var result = from m in messages
                         join f in faculties on m.FacultyId equals f.FacultyId
                         join u in users on m.UserId equals u.UserId
                         orderby m.CreatedAt descending
                         select new
                         {
                             MessageId = m.MessageId,
                             Content = m.Content,
                             CreatedAt = m.CreatedAt,
                             UserId = m.UserId,
                             UserName = u.Name,
                             Gmail = u.Gmail,
                             UserImage = u.Image == null ? "https://th.bing.com/th/id/OIP.HHVUf3TYqncgpJXyCMmxyAHaHa?pid=ImgDet&rs=1" : u.Image,
                             FacultyId = m.FacultyId,
                             FacultyName = f.Name
                         };
            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddMessage(string content, int userId, int facultyid)
        {
            await _messageService.Create(new Message() { Content = content, UserId = userId, CreatedAt = DateTime.Now, FacultyId = facultyid });
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _messageService.Delete(id);
            return Ok();
        }
    }
}
