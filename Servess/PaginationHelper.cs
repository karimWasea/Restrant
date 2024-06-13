using Interfaces;

using X.PagedList;

namespace Servess
{


    public class PaginationHelper<T> : IPaginationHelper<T> where T : class
    {
        public IPagedList<T> GetPagedData<T>(IQueryable<T> data, int pageNumber = 1)
        {
            //pageNumber = 1;
            int pageSize = 10; // Set the page size to 10
            int totalItemCount = data.Count();
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)pageSize);

            pageNumber = Math.Max(1, Math.Min(totalPages, pageNumber));

            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalItemCount - 1);

            var pagedData = data.Skip(startIndex).Take(pageSize).ToList();

            return new StaticPagedList<T>(pagedData, pageNumber, pageSize, totalItemCount);


        }

    }
}


