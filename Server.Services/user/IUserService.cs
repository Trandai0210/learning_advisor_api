using Server.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.user
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetList();
        Task<User> GetById(int id);
        Task Create(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
