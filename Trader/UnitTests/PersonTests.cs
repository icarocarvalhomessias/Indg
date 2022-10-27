using Moq;
using NUnit.Framework;
using System.Net;
using System.Xml.Linq;
using Trader.Api.Domain.Exceptions;
using Trader.Api.Domain.Models;
using Trader.Api.Repositories.Interfaces;
using Trader.Api.Service.Interfaces;
using Trader.Api.Service.Services;

using Xunit;

namespace Trader.Api.UnitTests
{

    public class PersonTests
    {
        private PersonService _personService;
        private Mock<IPersonRepository> _personRepositoryMock;
        private Mock<IItemService> _itemServiceMock;

        public PersonTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            //_itemServiceMock = new Mock<IItemService>();

            _personService = new PersonService(_personRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            // Arrange
            _personRepositoryMock.Setup(r => r.Get()).ReturnsAsync(new List<Person>());

            // Act
            var result = await _personService.Get();

            // Assert
            Assert.IsNotNull(result);
        }

        [Fact]
        public async Task ShouldGetPersonById()
        {
            // Arrange
            _personRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Person());

            // Act
            var result = await _personService.Get(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [Fact]
        public async Task ShouldNotFindUser()
        {
            // Arrange
            _personRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((Person) null);

            // Act
            var exception = Assert.ThrowsAsync<ApiException>(async () => await _personService.Get(1));

            Assert.AreEqual(TraderApiError.Person_not_found, exception.StatusCode);
        }

        [Xunit.Theory]
        [InlineData(1, "Marcos")]
        public async Task ShouldUpdatePerson(int PersonId, string Name)
        {
            _personRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Person());
            _personRepositoryMock.Setup(r => r.Get()).ReturnsAsync(new List<Person>());


            await _personService.Update(PersonId, Name);

            _personRepositoryMock.Verify(r => r.Save(), Times.Once);
        }

        [Xunit.Theory]
        [InlineData(1, "Marcos")]
        public async Task ShouldNotUpdatePersonNameAlreadyExists(int PersonId, string Name)
        {
            _personRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Person());
            _personRepositoryMock.Setup(r => r.Get()).ReturnsAsync(new List<Person>() { new Person(Name) });

            // Act
            var exception = Assert.ThrowsAsync<ApiException>(async () => await _personService.Update(PersonId, Name));

            Assert.AreEqual(TraderApiError.Person_name_not_unique, exception.StatusCode);
        }

        [Fact]
        public async Task ShouldDeletePerson()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item { IsActive = true },
                new Item { IsActive = true },
                new Item { IsActive = true },
            };
            var person = new Person
            {
                IsActive = true,
                Items = items
            };

            _personRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(new Person());
            _personRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(person);

            await _personService.Delete(1);

            Assert.IsFalse(person.IsActive);
            Assert.IsTrue(items.All(i => !i.IsActive));
            _personRepositoryMock.Verify(r => r.Save(), Times.Once);
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
