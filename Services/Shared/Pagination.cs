using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Shared
{
    public interface IPagination<T> where T : class
    {
        List<T> PagedList(ICollection<T> list, int CurrentPage, int ItemsPerPage);
    }

    public class Pagination<T> : IPagination<T> where T : class 
    {
        public List<T> PagedList(ICollection<T> list, int CurrentPage, int ItemsPerPage)
            => list
                .ToList()
                .GetRange((CurrentPage - 1) * ItemsPerPage, ItemsPerPage);
    }
}
