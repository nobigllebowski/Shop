using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Core.BaseControllers
{
    [ApiController]
    [Route("swagger/v{version:apiVersion}/[controller]/[action]")]
    public abstract class ApiController : ControllerBase
    {
        //private readonly IMediator? _mediator;

        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>()!;
    }
}