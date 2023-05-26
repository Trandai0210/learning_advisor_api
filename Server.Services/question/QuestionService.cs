using Server.Domain.Models;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.question
{
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> _repository;
        public QuestionService(IRepository<Question> repository)
        {
            _repository = repository;
        }
        public async Task Create(Question question)
        {
            await _repository.Create(question);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<Question> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Question>> GetList()
        {
            return await _repository.GetAll();
        }

        public async Task Update(Question question)
        {
            await _repository.Update(question);
        }
    }
}
