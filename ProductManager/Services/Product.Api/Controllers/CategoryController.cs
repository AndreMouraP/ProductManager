using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Api.Abstrations;
using Product.Api.Mapper;
using Product.Api.ViewModel;

namespace Product.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController<TViewmodel> : ControllerBase
        where TViewmodel: CategoryViewModel
    {
        private readonly IService<TViewmodel> Categoryservice;

        public CategoryController(IService<TViewmodel> categoryservice)
        {
            Categoryservice = categoryservice ?? throw new ArgumentNullException(nameof(categoryservice));
        }

        // POST api/v1/category/items
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.BadRequest)]
        public IActionResult Items([FromBody] IEnumerable<TViewmodel> categories)
        {
            try
            {
                if (Categoryservice.Post(categories))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(ex.Message));
            }
        }

        // GET api/v1/category/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItems<IEnumerable<ProductViewModel>>), (int)HttpStatusCode.OK)]
        public IActionResult Items([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            try
            {
                var products = Categoryservice.Get();
                var mappedItems = products
                                    .OrderBy(c => c.Name)
                                        .Skip(pageSize * pageIndex)
                                        .Take(pageSize);

                var model = new PaginatedItems<IEnumerable<TViewmodel>>(pageIndex, pageSize, products.Count(), mappedItems);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(ex.Message));
            }
        }

        // GET api/v1/category/item[?id]
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItems<IEnumerable<ProductViewModel>>), (int)HttpStatusCode.OK)]
        public IActionResult Item([FromQuery]Guid id)
        {
            try
            {
                var product = Categoryservice.Get(id);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(ex.Message));
            }
        }


    }
}
