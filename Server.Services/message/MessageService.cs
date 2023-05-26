using Server.Domain.Models;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.message
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _repository;
        public MessageService(IRepository<Message> repository)
        {
            _repository = repository;
        }
        public async Task Create(Message message)
        {
            await _repository.Create(message);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<Message> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Message>> GetList()
        {
            return await _repository.GetAll();
        }

        public async Task<bool> Update(Message message)
        {
            await _repository.Update(message);
            return true;
        }
    }
}
