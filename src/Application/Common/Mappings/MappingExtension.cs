﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Mappings
{
    public static class MappingExtension
    {
        public static Task<PaginationEntity<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginationEntity<TDestination>.CreatePaginationEntityAsync(queryable.AsNoTracking(), pageNumber, pageSize);
    }
}
