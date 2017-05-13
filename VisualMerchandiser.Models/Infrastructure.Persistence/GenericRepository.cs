using System.Data.Entity;
using System.Linq;
using VisualMerchandiser.Models.Domain;
using VisualMerchandiser.Models.Domain.Repositories;

namespace VisualMerchandiser.Models.Infrastructure.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IAggregate
    {
        private readonly DbContext _context;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork.Context;
        }

        public T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> All()
        {
            return _context.Set<T>();
        }

        public void Remove(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Save(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.ApplyStateChanges();
        }

        public void SaveGraph(T entity)
        {
            if (entity.Id == default(int))
            {
                _context.Set<T>().Add(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}