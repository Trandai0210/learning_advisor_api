using Server.Domain.Models;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.faculty
{
    public class FalcultyService : IFacultyService
    {
        private readonly IRepository<Faculty> _repository;

        public FalcultyService(IRepository<Faculty> repository)
        {
            _repository = repository;
        }
        public async Task Create(Faculty faculty)
        {
            await _repository.Create(faculty);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<Faculty> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Faculty>> GetList()
        {
            return await _repository.GetAll();
        }

        public async Task<bool> Update(Faculty faculty)
        {
            await _repository.Update(faculty);
            return true;
        }
    }
}
