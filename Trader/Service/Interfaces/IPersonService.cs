using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trader.Api.Domain.Models;

namespace Trader.Api.Service.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> Get();
        Task<Person> Get(int id);
        Task Insert(string name);
        Task Update(Person person);
        Task Delete(int id);

    }
}
