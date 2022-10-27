using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Post(string ItemName, int PersonId )
        {
            await _ItemService.Insert(ItemName, PersonId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int itenId, string NewItemName)
        {
            await _ItemService.Update(itenId, NewItemName);
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
