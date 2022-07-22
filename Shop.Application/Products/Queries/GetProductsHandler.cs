using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products.Dto;
using Shop.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Products.Queries
{
    public class GetProductsHandler: IRequestHandler<GetProducts, IActionResult>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductsHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetProducts request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Products.AsQueryable();

            var queryResult = query.ProjectTo<ProductListDto>(_mapper.ConfigurationProvider);

            return new OkObjectResult(queryResult);
        }
    }
}
