using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core.Data;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public class EntityService<T> : IEntityService<T> where T : Entity
    {
        protected readonly IFlightPlannerDbContext _context;

        public EntityService(IFlightPlannerDbContext context)
        {
            _context = context;
        }
        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
