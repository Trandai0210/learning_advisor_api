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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        public QuestionController(IQuestionService questionService, IAnswerService answerService)
        {
            _questionService = questionService;
            _answerService = answerService;
        }
        
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListQuestion()
        {
            IEnumerable<Question> questions = await _questionService.GetList();
            IEnumerable<Answer> answers = await _answerService.GetList();
            var result = from q in questions
                         join a in answers on q.AnswerId equals a.AnswerId
                         select new
                         {
                             QuestionId = q.QuestionId,
                             Keyword = q.Keyword,
                             AnswerId = q.AnswerId,
                             AnswerContent = a.Content
                         };
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAnswer")]
        public async Task<IActionResult> GetAnswerByQuestion(string question)
        {
            if(question != null)
            {
                question = question.Trim();
                IEnumerable<Question> questions = await _questionService.GetList();
                try
                {
                    Question question1 = questions.First(q => q.Keyword.ToLower() == question.ToLower());
                    if (question1 != null)
                    {
                        Answer answer = await _answerService.GetById((int)question1.AnswerId);
                        return Ok(answer.Content);
                    }
                    return Ok();
                }
                catch (Exception)
                {
                    return Ok();
                }
            }
            return Ok("");
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddQuestion(string keyword, int answerId)
        {
            await _questionService.Create(new Question() { Keyword = keyword, AnswerId = answerId });
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateQuestion(int questionId, string keyword, int answerId)
        {
            Question question = await _questionService.GetById(questionId);
            question.Keyword = keyword;
            question.AnswerId = answerId;
            await _questionService.Update(question);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            Question question = await _questionService.GetById(id);
            if (question != null)
            {
                await _answerService.Delete((int)question.AnswerId);
                await _questionService.Delete(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
