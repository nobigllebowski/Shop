using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products.Queries;
using Shop.Core.BaseControllers;
using System.Threading.Tasks;


namespace Shop.Api.Web.Controllers.Products
{
    [ApiVersion("1.0")]
    public class ProductController: ApiController
    {
        /// <summary>
        /// Получение списка товаров
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return await Mediator.Send(new GetProducts());
        }
    }
}
