using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Domain.Models;
using Server.Services.answer;
using Server.Services.question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListAnswer()
        {
            IEnumerable<Answer> answers = await _answerService.GetList();
            var result = from a in answers
                         orderby a.AnswerId ascending
                         select new
                         {
                             AnswerId = a.AnswerId,
                             Content = a.Content
                         };
            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAnswer(string content)
        {
            IEnumerable<Answer> answers = await _answerService.GetList();
            bool checkContent = answers.Any(a => a.Content != content);
            if(content != null && checkContent == true)
            {
                Answer answer = new Answer() { Content = content };
                await _answerService.Create(answer);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAnswer(int answerId, string content)
        {
            IEnumerable<Answer> answers = await _answerService.GetList();
            bool checkContent = answers.Any(a => a.Content != content);
            if(content != null && checkContent == true)
            {
                Answer answer = await _answerService.GetById(answerId);
                answer.Content = content;
                await _answerService.Update(answer);
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            Answer answer = await _answerService.GetById(id);
            if(answer != null)
            {
                await _answerService.Delete(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
