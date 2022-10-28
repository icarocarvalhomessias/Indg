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

        public ItemTransferService(IItemTransferRepository ItemTransferRepository, 
            IPersonService PersonService,
            IItemService ItemService)
        {
            _itemTransferRepository = ItemTransferRepository;
            _personService = PersonService;
            _itemService = ItemService;
        }

        public async Task<IEnumerable<ItemTransfer>> Get()
        {
            return await _itemTransferRepository.Get();
        }

        public async Task Transfer(int FromPersonId, int ToPersonId, int ItemId)
        {
            var ItemTransfer = new ItemTransfer();
            ItemTransfer.FromPersonId = FromPersonId;
            ItemTransfer.ToPersonId = ToPersonId;
            ItemTransfer.ItemId = ItemId;

            await ValidPersonsToTransfer(ItemTransfer);
            var item = await Get(ItemTransfer.ItemId);

            if(item.PersonId != ItemTransfer.FromPersonId)
                throw new ApiException("From Person is not the same of the item", TraderApiError.Person_is_not_the_same_of_item);


            item.PersonId = ItemTransfer.ToPersonId;
            await _itemTransferRepository.Insert(ItemTransfer);

            await _itemService.ChangeOwner(item);
        }


        private async Task<Item> Get(int ItemId)
        {
            var item = await _itemService.Get(ItemId);

            if (item?.IsActive == true)
                return item;

            throw new ApiException("Item inactive", TraderApiError.Item_inactive);

        }

        private async Task ValidPersonsToTransfer(ItemTransfer ItemTransfer)
        {
            var FromPerson = await _personService.Get(ItemTransfer.FromPersonId);
            var ToPerson = await _personService.Get(ItemTransfer.ToPersonId);

            if (!FromPerson.IsActive)
            {
                throw new ApiException("From Person is inactive", TraderApiError.Person_inactive);
            }

            if (!ToPerson.IsActive)
            {
                throw new ApiException("To Person is inactive", TraderApiError.Person_inactive);
            }
            // I can use this for a future logs and item history
        }
    }
}
