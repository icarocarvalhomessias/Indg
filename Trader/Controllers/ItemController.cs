using Microsoft.AspNetCore.Mvc;

using Trader.Api.Domain.Models;
using Trader.Api.Service.Interfaces;

namespace Trader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly IItemService _ItemService;

        public ItemController(IItemService personService)
        {
            _ItemService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _ItemService.Get();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string Name)
        {
            await _ItemService.Insert(Name);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Item Item)
        {
            await _ItemService.Update(Item);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _ItemService.Delete(id);
            return Ok();
        }
    }
}
