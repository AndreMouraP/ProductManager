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
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
                return BadRequest(ex.Data);
            }
        }

        // GET api/v1/category/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PaginatedItems<CategoryViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult Items([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            return Ok(/*new Items().Get(this.userRepository, pageSize, pageIndex)*/);
        }


    }
}
