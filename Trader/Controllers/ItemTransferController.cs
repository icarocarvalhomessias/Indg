using Microsoft.AspNetCore.Mvc;
using Trader.Api.Domain.Models;
using Trader.Api.Service.Interfaces;

namespace Trader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTransferController : ControllerBase
    {

        private readonly IItemTransferService _itemTransferService;

        public ItemTransferController(IItemTransferService itemTransferService)
        {
            _itemTransferService = itemTransferService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _itemTransferService.Get();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int FromPersonId, int ToPersonId, int ItemId)
        {
       
            await _itemTransferService.Transfer(FromPersonId, ToPersonId, ItemId);
            return Ok();
        }
    }
}
