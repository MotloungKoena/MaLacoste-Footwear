using MaLacoste_Footwear.Data.DataAccess;
using MaLacoste_Footwear.Models;
using System.Linq;
using System.Linq.Expressions;

namespace MaLacoste_Footwear.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _appDbCOntext;
        public RepositoryBase(AppDbContext appDbContext)
        {
            _appDbCOntext = appDbContext;
        }

        public void Create(T entity)
        {
            _appDbCOntext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _appDbCOntext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> FindAll()
        {
            return _appDbCOntext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _appDbCOntext.Set<T>().Where(expression);
        }

        public T GetById(int id)
        {
            return _appDbCOntext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetWithOptions(QueryOptions<T> options)
        {
            IQueryable<T> query = _appDbCOntext.Set<T>();

            if (options.HasWhere)
                query = query.Where(options.Where);

            if (options.HasOrderBy)
            {
                if (options.OrderByDirection is "asc")
                    query = query.OrderBy(options.OrderBy);
                else
                    query = query.OrderByDescending(options.OrderBy);
            }

            if (options.HasPaging)
            {
                query = query.Skip((options.PageNumber - 1) * options.PageSize)
                    .Take(options.PageSize);
            }

            return query.ToList();
        }

        public void Update(T entity)
        {
            _appDbCOntext.Set<T>().Update(entity);
        }
    }
}
