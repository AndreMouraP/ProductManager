using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Api.Abstrations;
using Product.Api.Mapper;
using Product.Api.ViewModel;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController<TViewmodel> : ControllerBase
        where TViewmodel : ProductViewModel
    {
        private readonly IService<TViewmodel> Productservice;

        public ProductController(IService<TViewmodel> productservice)
        {
            Productservice = productservice ?? throw new ArgumentNullException(nameof(productservice));
        }

        // POST api/v1/product/items
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.BadRequest)]
        public IActionResult Items([FromBody] IEnumerable<TViewmodel> products)
        {
            try
            {
                if (Productservice.Post(products))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(ex.Message));
            }
        }

        // GET api/v1/product/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItems<IEnumerable<ProductViewModel>>), (int)HttpStatusCode.OK)]
        public IActionResult Items([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            try
            {
                var products = Productservice.Get();
                var mappedItems = products
                                    .OrderBy(c => c.Name)
                                        .Skip(pageSize * pageIndex)
                                        .Take(pageSize);

                var model = new PaginatedItems<IEnumerable<TViewmodel>>(pageIndex, pageSize, products.Count(), mappedItems);
                return Ok(model);               
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel (ex.Message));
            }
        }

        // GET api/v1/product/item[?id]
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItems<IEnumerable<ProductViewModel>>), (int)HttpStatusCode.OK)]
        public IActionResult Item([FromQuery]Guid id)
        {
            try
            {
                var product = Productservice.Get(id);         

                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(ex.Message));
            }
        }
    }
}
