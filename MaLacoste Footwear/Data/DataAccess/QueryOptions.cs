using System.Linq.Expressions;

namespace MaLacoste_Footwear.Data.DataAccess
{
    public class QueryOptions<T>
    {
        //public properties for sorting, filtering and paging
        public Expression<Func<T, object>> OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc"; //default
        public Expression<Func<T, bool>> Where { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }


        //read-only properties
        public bool HasWhere => Where is not null;
        public bool HasOrderBy => OrderBy is not null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;
    }
}
