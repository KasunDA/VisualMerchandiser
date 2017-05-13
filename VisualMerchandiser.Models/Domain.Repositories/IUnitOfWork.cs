using System.Data.Entity;
using System.Threading.Tasks;

namespace VisualMerchandiser.Models.Domain.Repositories
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        int Save();
        Task<int> SaveAsync();
    }
}