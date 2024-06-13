using Cf_Viewmodels;

using X.PagedList;

namespace Interfaces
{
    public interface IPaginationHelper<T>
    {
        IPagedList<T> GetPagedData<T>(IQueryable<T> data, int pagenumber);
    }
}
