using Server.Domain.Models;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.user
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task Create(User user)
        {
            await _repository.Create(user);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<User> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<User>> GetList()
        {
            return await _repository.GetAll();
        }

        public async Task Update(User user)
        {
            await _repository.Update(user);
        }
    }
}
