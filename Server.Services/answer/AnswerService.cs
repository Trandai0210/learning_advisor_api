using Server.Domain.Models;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.answer
{
    public class AnswerService : IAnswerService
    {
        private readonly IRepository<Answer> _repository;
        public AnswerService(IRepository<Answer> repository)
        {
            _repository = repository;
        }

        public async Task Create(Answer answer)
        {
            await _repository.Create(answer);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<Answer> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Answer>> GetList()
        {
            return await _repository.GetAll();
        }

        public async Task<bool> Update(Answer answer)
        {
            await _repository.Update(answer);
            return true;
        }
    }
}
