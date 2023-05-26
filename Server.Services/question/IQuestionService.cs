using Server.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.question
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetList();
        Task<Question> GetById(int id);
        Task Create(Question question);
        Task Update(Question question);
        Task Delete(int id);
    }
}
