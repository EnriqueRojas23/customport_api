using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Common;
using Api.Data;
using Api.Data.Interface;
using Api.Domain.Seguridad;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.Handlers.Seguridad
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DataContext _context;
        private readonly DbSet<T> dbSet; //here
        private readonly IConfiguration _config;

        public Repository(DataContext context,IConfiguration config)
        {
            _context = context;
            dbSet = _context.Set<T>();
             _config = config;

        }
        public IDbConnection Connection
        {   
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Delete(T entity) {  _context.Remove(entity); _context.SaveChanges();  }

        public void DeleteAll(IEnumerable<T> entity) {  _context.RemoveRange(entity);  _context.SaveChanges(); }

        public async Task<T> Get(Expression<Func<T, bool>> predicate) 
        {
            return await dbSet.Where(predicate).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}