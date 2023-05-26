using Microsoft.EntityFrameworkCore;
using Server.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CoVanHocTapContext _context;
        private DbSet<T> entities;
        public Repository(CoVanHocTapContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }
        public async Task<T> Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Entity");
            }
            await entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            entities.Remove(entities.Find(id));
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Entity");
            }
            entities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
