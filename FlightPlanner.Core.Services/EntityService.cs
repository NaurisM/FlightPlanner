using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public class EntityService<T> : IEntityService<T> where T : Entity
    {
        public IQueryable<T> Query()
        {
            return Query();
        }

        public IEnumerable<T> Get()
        {
            return Get();
        }

        public T GetById(int id)
        {
            return GetById(id);
        }

        public void Create(T entity)
        {
            Create(entity);
        }

        public void Update(T entity)
        {
            Update(entity);
        }

        public void Delete(T entity)
        {
            Delete(entity);
        }
    }
}
