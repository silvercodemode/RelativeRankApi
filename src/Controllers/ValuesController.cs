using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RelativeRank.EntityFrameworkEntities;
using RelativeRank.Interfaces;

namespace RelativeRank.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRelativeRankRepository _repository;

        public ValuesController(IRelativeRankRepository repository) => _repository = repository;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Show>> GetAllShows()
        {
            return _repository.GetAllShows();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
