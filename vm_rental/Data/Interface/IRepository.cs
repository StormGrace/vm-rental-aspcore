using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vm_rental.Data.Interface
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
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
