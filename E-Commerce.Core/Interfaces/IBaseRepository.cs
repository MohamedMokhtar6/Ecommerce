﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Add(T entry);
        Task Delet(T item);
        Task DeleteAll(IEnumerable<T> items);
        Task<T> FindById(int id);
        Task<T> FindById(string id);
        Task<T> FindById(Guid id);
        Task<IEnumerable<T>> GetAll();

        void Update(T item);
        Task<IEnumerable<T>> FindAllByQuery(Expression<Func<T, bool>> match );
        Task<IEnumerable<T>> FindAllByQuery(Expression<Func<T, bool>> match ,String[] includes = null);
        Task<IEnumerable<T>> FindAllByQuery(Expression<Func<T, bool>> match, String[] includes = null ,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = "Ascending" );
        Task<T> FindByQuery(Expression<Func<T,bool>> match, String[] includes = null);
        Task<IEnumerable<T>> GetAllByQuery(int? skip, int? take, String[] includes = null , Expression<Func<T, object>> orderBy = null, string orderByDirection = "Ascending");
        Task<IEnumerable<T>> GetAllByQuery(String[] includes = null);
        Task<int> count();
    }
}
