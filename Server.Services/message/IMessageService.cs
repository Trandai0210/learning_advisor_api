using Server.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.message
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetList();
        Task<Message> GetById(int id);
        Task Create(Message message);
        Task<bool> Update(Message message);
        Task Delete(int id);
    }
}
