using Server.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.faculty
{
    public interface IFacultyService
    {
        Task<IEnumerable<Faculty>> GetList();
        Task<Faculty> GetById(int id);
        Task Create(Faculty faculty);
        Task<bool> Update(Faculty faculty);
        Task Delete(int id);
    }
}
