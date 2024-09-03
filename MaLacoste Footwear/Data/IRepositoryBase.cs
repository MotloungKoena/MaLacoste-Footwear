using MaLacoste_Footwear.Data.DataAccess;
using System.Linq.Expressions;

namespace MaLacoste_Footwear.Data
{
    public interface IRepositoryBase<T> //generic, the type will be determined during runtime
    {
        T GetById(int id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetWithOptions(QueryOptions<T> options);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
