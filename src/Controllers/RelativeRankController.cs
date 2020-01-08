using Microsoft.AspNetCore.Mvc;
using RelativeRank.Entities;
using RelativeRank.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelativeRank.Controllers
{
    [ApiController]
    [Route("/")]
    public class RelativeRankController : ControllerBase
    {
        private readonly IShowRepository _repository;

        public RelativeRankController(IShowRepository repository) => _repository = repository;

        public async Task<ActionResult<IEnumerable<RankedShow>>> GetAllShows()
        {
            var rankedShows = await _repository.GetAllShows().ConfigureAwait(false);

            return Ok(rankedShows.Select(show => new RelativeRankedShow
            {
                Name = show.Name,
                PercentileRank = show.PercentileRank
            }));
        }
    }
}
