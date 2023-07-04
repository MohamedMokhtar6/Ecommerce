using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class 
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entry)
        {
            await _context.Set<T>().AddAsync(entry);
             _context.SaveChanges();
            return entry;
        }

        public async Task Delet(T item)
        {
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
 
        }    public async Task DeleteAll(IEnumerable<T> items)
        {
            _context.Set<T>().RemoveRange(items);
            _context.SaveChanges();
 
        }

        public async Task<T> FindById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T> FindById(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }  public async Task<T> FindById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async  Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();  
        }

        public async void Update(T item)
        {
            _context.Set<T>().Update(item);
            _context.SaveChanges();
        }
        public async Task<IEnumerable<T>> FindAllByQuery(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            return await query.Where(match).ToListAsync();
        }  public async Task<IEnumerable<T>> FindAllByQuery(Expression<Func<T, bool>> match)
        {
            IQueryable<T> query = _context.Set<T>();
          
            return await query.Where(match).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllByQuery(Expression<Func<T, bool>> match, string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = "Ascending")
        {
            IQueryable<T> query = _context.Set<T>();
            if(includes != null)
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            return await query.Where(match).ToListAsync();
        }

        public async Task<T> FindByQuery(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            return await query.FirstOrDefaultAsync(match);
        }

        public async Task<IEnumerable<T>> GetAllByQuery(string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = "Ascending")
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                };
            if (orderBy != null)
            {
                if (orderByDirection == "Ascending")
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByQuery(string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if( includes != null)
                foreach (var include in includes)
                {
                    query=query.Include(include);
                }
            return await query.ToListAsync();
        }
    }
}
