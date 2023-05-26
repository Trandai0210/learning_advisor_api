using Server.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.permission
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetList();
        Task<Permission> GetById(int id);
        Task Create(Permission permission);
        Task Update(Permission permission);
        Task Delete(int id);
    }
}
