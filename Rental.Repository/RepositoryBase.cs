using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Repository
{
    public class RepositoryBase<T> where T : class
    {
        protected DbContext _context = null;

        public RepositoryBase(DbContext dbContext)
        {
            _context = dbContext;
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

    }
}
