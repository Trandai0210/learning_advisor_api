using Server.Domain.Models;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<Permission> _repository;
        public PermissionService(IRepository<Permission> repository)
        {
            _repository = repository;
        }
        public async Task Create(Permission permission)
        {
            await _repository.Create(permission);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<Permission> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Permission>> GetList()
        {
            return await _repository.GetAll();
        }

        public async Task Update(Permission permission)
        {
            await _repository.Update(permission);
        }
    }
}
