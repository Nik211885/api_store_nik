﻿using Application.Common;
using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Ratings.Queries
{
    public record GetRatingForProductWithPaginationQuery(string ProductId, int PageNumber = 1, int PageSize = 20) : IRequest<PaginationEntity<RatingReponse>>;
}
