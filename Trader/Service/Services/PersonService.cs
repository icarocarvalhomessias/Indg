using Trader.Api.Domain.Exceptions;
using Trader.Api.Domain.Models;
using Trader.Api.Repositories.Interfaces;
using Trader.Api.Service.Interfaces;

namespace Trader.Api.Service.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _personRepository { get; set; }

        public PersonService(IPersonRepository PersonRepository)
        {
            _personRepository = PersonRepository;
        }

        public async Task<IEnumerable<Person>> Get()
        {
            return await _personRepository.Get();
        }

        public async Task<Person> Get(int Id)
        {
            var person = await _personRepository.Get(Id);
            if (person != null)
                return person;

            throw new ApiException("Person not fouond for the given Id", TraderApiError.Person_not_found);
        }

        public async Task Insert(string Name)
        {
            var person = new Person(Name);

            await IsUnique(person);

            await _personRepository.Insert(person);

            await _personRepository.Save();
        }

        private async Task IsUnique(Person Person)
        {
            var existPerson = await _personRepository.Get(Person.Id);

            if (existPerson != null)
                throw new ApiException("Already exists a person with this Name", TraderApiError.Person_name_not_unique);
        }

        public async Task Update(int PersonId, string NewName)
        {
            var currentPerson = await Get(PersonId);
            
            await IsUnique(NewName);

            currentPerson.Name = NewName;

            await _personRepository.Save();
        }

        private async Task IsUnique(string Name)
        {
            var existsPersons = await _personRepository.Get();

            if(existsPersons.Any(x => x.Name == Name))
            {
                throw new ApiException("Already exists a person with this Name", TraderApiError.Person_name_not_unique);
            }
        }

        public async Task Delete(int Id)
        {
            var person = await Get(Id);
            person.IsActive = false;

            await InativateItens(person);

            await _personRepository.Save();
        }

        private async Task InativateItens(Person Person)
        {
            if (Person.Items?.Any() == true)
            {
                foreach (var item in Person.Items)
                {
                    item.IsActive = false;
                }
            }
        }
    }
}
