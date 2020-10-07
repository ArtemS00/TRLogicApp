using System.Linq;

namespace TRLogicApp.Services
{
    public interface IRepository<IEntity>
    {
        IQueryable<IEntity> Get();
        IEntity Get(int id);
        void Add(IEntity entity);
        void Delete(int id);
        void Update(int id, IEntity entity);
    }
}
