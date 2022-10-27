using Moq;
using NUnit.Framework;
using Trader.Api.Domain.Exceptions;
using Trader.Api.Domain.Models;
using Trader.Api.Repositories.Interfaces;
using Trader.Api.Service.Interfaces;
using Trader.Api.Service.Services;
using Xunit;

namespace Trader.Api.UnitTests
{
    public class ItemTests
    {
        private ItemService _itemService;

        private Mock<IItemRepository> _itemRepository;
        private Mock<IPersonService> _personService;


        public ItemTests()
        {
            _itemRepository = new Mock<IItemRepository>();
            _personService = new Mock<IPersonService>();

            _itemService = new ItemService(_itemRepository.Object, _personService.Object);
        }


        [Fact]
        public async Task ShouldGetAll()
        {
            // Arrange
            _itemRepository.Setup(r => r.Get()).ReturnsAsync(new List<Item>());

            // Act
            var result = await _itemService.Get();

            // Assert
            Assert.IsNotNull(result);
        }

        [Fact]
        public async Task ShouldGetItemById()
        {
            // Arrange
            _personService.Setup(r => r.Get()).ReturnsAsync(new List<Person>());
            _itemRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Item());

            // Act
            var result = await _itemService.Get(It.IsAny<int>());

            // Assert
            Assert.IsNotNull(result);
        }

        [Fact]
        public async Task ShouldNotFound()
        {
            // Arrange
            _personService.Setup(r => r.Get()).ReturnsAsync(new List<Person>());
            _itemRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((Item)null);

            // Act
            var exception = Assert.ThrowsAsync<ApiException>(async () => await _itemService.Get(It.IsAny<int>()));

            Assert.AreEqual(TraderApiError.Item_not_found, exception.StatusCode);

        }

        [Fact]
        public async Task ShouldInserItem()
        {
            var name = "Icaro";
            var personId = It.IsAny<int>();

            _itemRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Item());
            _personService.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Person());
            

            await _itemService.Insert(name, personId);

            _itemRepository.Verify(r => r.Insert(It.IsAny<Item>()), Times.Once);
            _itemRepository.Verify(r => r.Save(), Times.Once);

        }


        [Fact]
        public async Task ShouldNotInserItemInactivePerson()
        {
            var name = "Icaro";
            var personId = It.IsAny<int>();
            var person = new Person();
            person.IsActive = false;

            _itemRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Item());
            _personService.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(person);

            var exception = Assert.ThrowsAsync<ApiException>(async () => await _itemService.Insert(name, personId));

            Assert.AreEqual(TraderApiError.Person_inactive, exception.StatusCode);
        }

        [Fact]
        public async Task ShouldChangeOwnerItem()
        {
            var item = new Item();
            item.Name = "Pneu";

            _itemRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(item);

            await _itemService.ChangeOwner(item);

            _itemRepository.Verify(r => r.Save(), Times.Once);

        }

        [Fact]
        public async Task ShouldNotChangeOwnerItemChanceItemName()
        {
            var item = new Item();
            item.Name = "Pneu";

            _itemRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Item());

            var exception = Assert.ThrowsAsync<ApiException>(async () => await _itemService.ChangeOwner(item));

            Assert.AreEqual(TraderApiError.change_Owner_with_item_different_name, exception.StatusCode);

            _itemRepository.Verify(r => r.Save(), Times.Never);

        }

        [Fact]
        public async Task ShouldUpdateItem()
        {
            _itemRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Item());
            _itemRepository.Setup(r => r.Get()).ReturnsAsync(new List<Item>());

            await _itemService.Update(It.IsAny<int>(), "teste");

            _itemRepository.Verify(r => r.Save(), Times.Once);

        }

        [Fact]
        public async Task ShouldNotUpdateItemAlreadyExists()
        {
            var item = new Item("Rails", 1);

            _itemRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Item());
            _itemRepository.Setup(r => r.Get()).ReturnsAsync(new List<Item>() { item });

            var exception = Assert.ThrowsAsync<ApiException>(async () => await _itemService.Update(1, "Rails"));

            Assert.AreEqual(TraderApiError.Item_name_not_Unique, exception.StatusCode);
        }


        [Fact]
        public async Task ShouldDeleteItem()
        {
            _itemRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Item());

            await _itemService.Delete(It.IsAny<int>());

            _itemRepository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public async Task ShouldInativateItens()
        {
            var items = new List<Item>
            {
                new Item { IsActive = true },
                new Item { IsActive = true },
                new Item { IsActive = true },
            };

            _itemRepository.Setup(r => r.GetByPersonId(It.IsAny<int>())).ReturnsAsync(items);

            await _itemService.InativateItens(It.IsAny<int>());

            Assert.IsTrue(items.All(i => !i.IsActive));
            _itemRepository.Verify(r => r.Save(), Times.Once);
        }
    }
}
