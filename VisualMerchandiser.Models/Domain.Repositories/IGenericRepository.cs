using System.Linq;

namespace VisualMerchandiser.Models.Domain.Repositories
{
    public interface IGenericRepository<T> where T : IAggregate
    {
        T FindById(int id);
        IQueryable<T> All();
        void Remove(T entity);
        void Save(T entity);
        void SaveGraph(T entity);
    }
}