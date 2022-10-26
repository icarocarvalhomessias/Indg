using System.Net;
using Trader.Api.Domain.Exceptions;
using Trader.Api.Domain.Models;
using Trader.Api.Repositories.Interfaces;
using Trader.Api.Service.Interfaces;

namespace Trader.Api.Service.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Item>> Get()
        {
            return await _itemRepository.Get();
        }

        public async Task<Item> Get(int Id)
        {
            var item = await _itemRepository.Get(Id);
            if (item != null)
                return item;

            throw new ApiException("Item not found for given Id", HttpStatusCode.NotFound);
        }

        public async Task Insert(string Name)
        {
            var item = new Item(Name);

            await IsUnique(item);

            await _itemRepository.Insert(item);

            await _itemRepository.Save();
        }

        private async Task IsUnique(Item Item)
        {
            var existsItem = await _itemRepository.Get(Item.Id);

            if (existsItem != null)
                throw new ApiException("An Item with this Name already exists", HttpStatusCode.Ambiguous);
        }

        public async Task Update(Item Item)
        {
            var currentItem = await Get(Item.Id);
            currentItem.Name = Item.Name;

            await IsUnique(currentItem);

            await _itemRepository.Save();
        }

        public async Task Delete(int Id)
        {
            var person = await Get(Id);

            person.IsActive = false;

            await _itemRepository.Save();
        }

        public async Task InativateItens(int PersonId)
        {
            var PersonItems = await _itemRepository.GetByPersonId(PersonId);

            foreach (var item in PersonItems)
                item.IsActive = false;

            await _itemRepository.Save();
        }
    }

}