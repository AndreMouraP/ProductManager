using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Abstrations;
using Products.Api.Mapper;
using Products.Api.ViewModel;

namespace Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<ProductViewModel> _productService;
        private readonly string _deleteErrorMessage = "One or more Ids do not exist in database";
        private readonly string _postErrorMessage = "One or more Ids already exist in database, please make sure your Ids are unique";

        public ProductController(IService<ProductViewModel> productservice)
        {
            _productService = productservice ?? throw new ArgumentNullException(nameof(productservice));
        }

        // POST api/v1/product/items
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.BadRequest)]
        public IActionResult Items([FromBody] IEnumerable<ProductViewModel> products)
        {
            try
            {
                if (_productService.Post(products))
                    return Ok();
                else
                    return BadRequest(new ErrorModel(_postErrorMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(ex.Message));
            }
        }

        // DELETE api/v1/product/items
        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.BadRequest)]
        public IActionResult Items([FromBody] IEnumerable<string> ids)
        {
            try
            {
                if (_productService.Delete(ids))
                    return Ok();
                else
                    return BadRequest(new ErrorModel(_deleteErrorMessage));
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
                var products = _productService.Get();
                var mappedItems = products
                                    .OrderBy(c => c.Name)
                                        .Skip(pageSize * pageIndex)
                                        .Take(pageSize);

                var model = new PaginatedItems<IEnumerable<ProductViewModel>>(pageIndex, pageSize, products.Count(), mappedItems);
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
        public IActionResult Item([FromQuery]string id)
        {
            try
            {
                var product = _productService.Get(id);         

                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(ex.Message));
            }
        }
    }
}
