using Microsoft.AspNetCore.Routing;

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

    public class PersonUseCasesTests
    {
        private PersonService _personService;
        private Mock<IPersonRepository> _personRepositoryMock;
        private Mock<IItemService> _itemServiceMock;

        public PersonUseCasesTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _itemServiceMock = new Mock<IItemService>();

            _personService = new PersonService(_personRepositoryMock.Object, _itemServiceMock.Object);
        }

        [Fact]
        public async Task ShouldGetPersonById()
        {
            // Arrange
            _personRepositoryMock.Setup(r => r.Get()).ReturnsAsync(new List<Person>());

            // Act
            var result = await _personService.Get();

            // Assert
            Assert.IsNotNull(result);
        }


        [Fact]
        public async Task ShouldInsertPerson()
        {
            // Arrange
            _personRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((Person)null);

            // Act
            await _personService.Insert("Icaro");

            // Assert
            _personRepositoryMock.Verify(r => r.Insert(It.IsAny<Person>()), Times.Once);
            _personRepositoryMock.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public async Task ShouldNotInsertPersonWithSameName()
        {
            // Arrange
            _personRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Person());

            // Act
            var exception = Assert.ThrowsAsync<ApiException>(async () => await _personService.Insert("Icaro"));


            // Assert
            Assert.AreEqual("Already exists a person with this Name", exception.Erro);
        }
    }
}
