using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common
{
    public class PaginationEntity<T> where T : notnull
    {
        public IEnumerable<T> Items { get; }
        /// <summary>
        /// Page number must>1
        /// </summary>
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalItems { get; }
        private PaginationEntity(IEnumerable<T> items, int pageNumber,
            int pageSize, int totalItems
            )
        {
            PageNumber = pageNumber;
            PageSize = (int)Math.Ceiling((decimal)totalItems / pageNumber);
            TotalItems = totalItems;
            Items = items;
        }
        public static async Task<PaginationEntity<T>> CreatePaginationEntityAsync(IQueryable<T> query, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber -1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginationEntity<T>(items,pageNumber,pageSize,totalCount);
        }
    }
}
