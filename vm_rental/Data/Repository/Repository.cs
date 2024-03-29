﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm_rental.Data.Repository.Interface.Common;

namespace vm_rental.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly VmDbContext _context;

        public void SaveChanges() => _context.SaveChanges();

        public Repository(VmDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
                    SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
                     SaveChanges();
        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
           return _context.Set<T>().Where(predicate);
        }

        public T FindFirst(Func<T, bool> predicate)
        {
          return _context.Set<T>().FirstOrDefault(predicate);
        }
    
        public bool Exists(Func<T, bool> predicate)
        {
          return _context.Set<T>().Any(predicate);
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
           return _context.Set<T>().ToList();
        }

        public int Count(Func<T, bool> predicate)
        {
           return _context.Set<T>().Where(predicate).Count();
        }
    }
}
