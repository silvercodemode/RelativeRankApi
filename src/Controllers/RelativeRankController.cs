using Microsoft.AspNetCore.Mvc;
using RelativeRank.Entities;
using RelativeRank.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelativeRank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelativeRankController : ControllerBase
    {
        private readonly IShowRepository _repository;

        public RelativeRankController(IShowRepository repository) => _repository = repository;

        public async Task<ActionResult<IEnumerable<RankedShow>>> GetAllShows()
        {
            return await _repository.GetAllShows();
        }
    }
}
