using System;
using System.Collections.Generic;


namespace vm_rental.Data.Repository.Interface.Common
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        int Count(Func<T, bool> predicate);
        IEnumerable<T> Find(Func<T, bool> predicate);    
        T FindFirst(Func<T, bool> predicate);
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool Exists(Func<T, bool> predicate);
    }
}
