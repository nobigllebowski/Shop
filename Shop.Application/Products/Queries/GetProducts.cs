using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products.Dto;

namespace Shop.Application.Products.Queries
{
    public class GetProducts: ProductListDto, IRequest<IActionResult>
    {
    }
}
