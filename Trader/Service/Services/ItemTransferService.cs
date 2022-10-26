using System.Net;

using Trader.Api.Domain.Exceptions;
using Trader.Api.Domain.Models;
using Trader.Api.Repositories.Interfaces;
using Trader.Api.Service.Interfaces;

namespace Trader.Api.Service.Services
{
    public class ItemTransferService : IItemTransferService
    {
        private readonly IItemTransferRepository _itemTransferRepository;
        private readonly IPersonService _personService;
        private readonly IItemService _itemService;

        public ItemTransferService(IItemTransferRepository itemTransferRepository, 
            IPersonService personService,
            IItemService itemService)
        {
            _itemTransferRepository = itemTransferRepository;
            _personService = personService;
            _itemService = itemService;
        }

        public async Task<IEnumerable<ItemTransfer>> Get()
        {
            return await _itemTransferRepository.Get();
        }

        public async Task Transfer(ItemTransfer ItemTransfer)
        {
            await ValidPersonsAtTheTransfer(ItemTransfer);
            var item = await GetItem(ItemTransfer.ItemId);

            item.PersonId = ItemTransfer.ToPersonId;
            await _itemTransferRepository.Insert(ItemTransfer);

            await _itemService.Update(item);
        }


        private async Task<Item> GetItem(int ItemId)
        {
            var item = await _itemService.Get(ItemId);

            if (item?.IsActive == true)
                return item;

            throw new ApiException("Item inactive", HttpStatusCode.NotFound);

        }

        private async Task ValidPersonsAtTheTransfer(ItemTransfer itemTransfer)
        {
            var FromPerson = await _personService.Get(itemTransfer.FromPersonId);
            var ToPerson = await _personService.Get(itemTransfer.ToPersonId);

            if (!FromPerson.IsActive)
            {
                throw new ApiException("From Person is inactive", HttpStatusCode.NotFound);
            }

            if (!ToPerson.IsActive)
            {
                throw new ApiException("To Person is inactive", HttpStatusCode.NotFound);
            }


            // I can use this for a future logs and item history
        }
    }
}
