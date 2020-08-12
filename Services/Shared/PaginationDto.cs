using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared
{
    public interface IPagination<T> where T : class
    {

    }

    public class PaginationDto<T> : IPagination<T> where T : class
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ICollection<T> List { get; set; }
        public int TotalPages { get; set; }
        public PaginationDto()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationDto(ICollection<T> list, int pageNumber, int pageSize)
        {

            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;

            this.PageSize = pageSize > 10 ? 10 : pageSize;

            this.TotalPages =
                (int)Math.Ceiling((decimal)list.Count / (decimal)pageSize);

            this.List = list.Skip((this.PageNumber - 1) * this.PageSize)
            .Take(this.PageSize)
            .ToList();
        }

    }
}