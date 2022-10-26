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

        private readonly IPersonService _personService;

        public ItemService(IItemRepository itemRepository, IPersonService personService)
        {
            _itemRepository = itemRepository;
            _personService = personService;
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

        public async Task Insert(string ItemName, int PersonId)
        {
            var item = new Item(ItemName, PersonId);
            await IsUnique(item);

            await IsValidPerson(item.PersonId);

            await _itemRepository.Insert(item);

            await _itemRepository.Save();
        }

        private async Task IsValidPerson(int PersonId)
        {
            var person = await _personService.Get(PersonId);

            if(!person.IsActive)
                throw new ApiException("This person is inactive", HttpStatusCode.Ambiguous);
        }

        private async Task IsUnique(Item Item)
        {
            var existsItem = await _itemRepository.Get(Item.Id);

            if (existsItem != null && Item.Id != existsItem.Id)
                throw new ApiException("An Item with this Name already exists", HttpStatusCode.Ambiguous);
        }

        public async Task Update(Item Item)
        {
            var currentItem = await Get(Item.Id);
            currentItem.Name = Item.Name;

            await IsUnique(currentItem);

            await _itemRepository.Save();
        }

        public async Task Update(int itenId, string NewItemName)
        {
            var currentItem = await Get(itenId);
            currentItem.Name = NewItemName;

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