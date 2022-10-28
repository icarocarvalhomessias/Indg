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
    public class ItemTransferTests
    {
        private ItemTransferService _itemTransferService;

        private Mock<IItemTransferRepository> _itemTransferRepositoryMock;
        private Mock<IItemService> _itemServiceMock;
        private Mock<IPersonService> _personServiceMock;

        public ItemTransferTests()
        {
            _itemServiceMock = new Mock<IItemService>();
            _personServiceMock = new Mock<IPersonService>();
            _itemTransferRepositoryMock = new Mock<IItemTransferRepository>();

            _itemTransferService = new ItemTransferService(_itemTransferRepositoryMock.Object, _personServiceMock.Object, _itemServiceMock.Object);
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            // Arrange
            _itemTransferRepositoryMock.Setup(r => r.Get()).ReturnsAsync(new List<ItemTransfer>());

            // Act
            var result = await _itemTransferService.Get();

            // Assert
            Assert.IsNotNull(result);
        }

        [Fact]
        public async Task ShouldTransferItem()
        {
            var anyInt = It.IsAny<int>();
            // Arrange
            _itemTransferRepositoryMock.Setup(r => r.Get()).ReturnsAsync(new List<ItemTransfer>());
            _personServiceMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Person());
            _itemServiceMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Item());

            // Act
            await _itemTransferService.Transfer(anyInt, anyInt, anyInt);

            // Assert
            _itemTransferRepositoryMock.Verify(r => r.Insert(It.IsAny<ItemTransfer>()), Times.Once);

        }

        [Fact]
        public async Task ShouldNotTransferItemPersonInactive()
        {
            var anyInt = It.IsAny<int>();

            var person = new Person();
            person.IsActive = false;

            // Arrange
            _itemTransferRepositoryMock.Setup(r => r.Get()).ReturnsAsync(new List<ItemTransfer>());
            _personServiceMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(person);
            _itemServiceMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Item());

            var exception = Assert.ThrowsAsync<ApiException>(async () => await _itemTransferService.Transfer(anyInt, anyInt, anyInt));

            Assert.AreEqual(TraderApiError.Person_inactive, exception.StatusCode);
        }


        [Fact]
        public async Task ShouldNotTransferItemItemInactive()
        {
            var anyInt = It.IsAny<int>();

            var item = new Item();
            item.IsActive = false;

            // Arrange
            _itemTransferRepositoryMock.Setup(r => r.Get()).ReturnsAsync(new List<ItemTransfer>());
            _personServiceMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Person());
            _itemServiceMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(item);

            var exception = Assert.ThrowsAsync<ApiException>(async () => await _itemTransferService.Transfer(anyInt, anyInt, anyInt));

            Assert.AreEqual(TraderApiError.Item_inactive, exception.StatusCode);
        }
    }
}
