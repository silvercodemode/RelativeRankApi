using Microsoft.AspNetCore.Mvc;
using RelativeRank.Entities;
using RelativeRank.Interfaces;
using System.Collections.Generic;

namespace RelativeRank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelativeRankController : ControllerBase
    {
        private readonly IShowRepository _repository;

        public RelativeRankController(IShowRepository repository) => _repository = repository;

        public ActionResult<IEnumerable<RankedShow>> GetAllShows()
        {
            return _repository.GetAllShows();
        }
    }
}
