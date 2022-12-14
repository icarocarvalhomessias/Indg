using Microsoft.AspNetCore.Mvc;
using Trader.Api.Service.Interfaces;

namespace Trader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _personService.Get();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string Name)
        {
            await _personService.Insert(Name);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int PersonId, string NewName)
        {
            await _personService.Update(PersonId, NewName);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int PersonId)
        {
            await _personService.Delete(PersonId);
            return Ok();
        }
    }
}
