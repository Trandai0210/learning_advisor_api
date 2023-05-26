using Server.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.answer
{
    public interface IAnswerService
    {
        Task<IEnumerable<Answer>> GetList();
        Task<Answer> GetById(int id);
        Task Create(Answer answer);
        Task<bool> Update(Answer answer);
        Task Delete(int id);
    }
}
