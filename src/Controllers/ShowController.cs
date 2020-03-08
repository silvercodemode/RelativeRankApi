using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelativeRank.DataTransferObjects;
using RelativeRank.Entities;
using RelativeRank.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelativeRank.Controllers
{
    [ApiController]
    [Authorize]
    public class ShowController : ControllerBase
    {
        private readonly IShowRepository _repository;

        public ShowController(IShowRepository repository) => _repository = repository;

        [AllowAnonymous]
        [HttpGet("/")]
        public async Task<ActionResult<IEnumerable<RankedShow>>> GetAllShowsRelativelyRanked()
        {
            var rankedShows = await _repository.GetAllShowsRelativelyRanked().ConfigureAwait(false);

            return Ok(rankedShows
                .Select(show => new RelativeRankedShow(show.Name, show.PercentileRank))
                .OrderByDescending(show => show.PercentileRank));
        }

        [AllowAnonymous]
        [HttpGet("/show/all-shows")]
        public ActionResult<IEnumerable<Show>> GetAllShows() => Ok(_repository.GetAllShows());

        [HttpPost("/show/add-show")]
        public async Task<ActionResult<string>> AddShow(AddShowModel show)
        {
            var requestHasAdminUserClaim = User.Claims.Where(claim => claim.Type == "user" && claim.Value == "1").ToList().Count > 0;
            if (!requestHasAdminUserClaim)
            {
                return BadRequest($"You do not have permissions add shows.");
            }

            var showAdded = await _repository.AddShow(show).ConfigureAwait(false);
            if (!showAdded)
            {
                return StatusCode(500, "Failed to add show.");
            }

            return Ok("Show successfully added.");
        }

        [HttpDelete("/show/{showId}")]
        public async Task<ActionResult<string>> DeleteShow(int showId)
        {
            var requestHasAdminUserClaim = User.Claims.Where(claim => claim.Type == "user" && claim.Value == "1").ToList().Count > 0;
            if (!requestHasAdminUserClaim)
            {
                return BadRequest($"You do not have permissions delete shows.");
            }

            var showDeleted = await _repository.DeleteShow(showId).ConfigureAwait(false);
            if (!showDeleted)
            {
                return BadRequest($"Show with id: {showId} does not exist.");
            }

            return Ok("Show successfully deleted.");
        }
    }
}
