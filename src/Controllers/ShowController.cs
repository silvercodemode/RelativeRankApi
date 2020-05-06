using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RelativeRank.Config;
using RelativeRank.DataTransferObjects;
using RelativeRank.Entities;
using RelativeRank.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace RelativeRank.Controllers
{
    [ApiController]
    [Authorize]
    public class ShowController : ControllerBase
    {
        private readonly IShowRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public ShowController(IShowRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        [AllowAnonymous]
        [HttpGet("/import-from-mal")]
        public async Task<IActionResult> ImportFromMalV2(string username)
        {
            var resultsLength = 0;
            var offset = 0;
            var shows = new List<RankedShow>();
            var showNameSet = new HashSet<string>();

            do
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://myanimelist.net/animelist/{username}/load.json?offset={offset}status=2"))
                using (var httpClient = _httpClientFactory.CreateClient())
                using (var response = await httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    var usersMalAnimeListPageJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var parsedJson = JArray.Parse(usersMalAnimeListPageJson);

                    foreach (var showJson in parsedJson)
                    {
                        var showTitle = showJson.SelectToken("anime_title").Value<string>();
                        var showRank = showJson.SelectToken("score").Value<int>();
                        var watchingStatus = showJson.SelectToken("status").Value<int>();

                        if (watchingStatus == 2 || showRank != 0)
                        {
                            shows.Add(new RankedShow
                            {
                                Name = showTitle,
                                Rank = showRank
                            });

                            showNameSet.Add(showTitle);
                        }
                    }

                    resultsLength = parsedJson.Count;
                }

                offset += 300;
            }
            while (resultsLength > 0);

            var showsThatExistInDb = _repository.FilterSetToShowNamesThatExist(showNameSet);

            return Ok(new RankedShowList(shows
                .Where(show => showsThatExistInDb.Contains(show.Name))
                .OrderByDescending(show => show.Rank)
                .Select((show, index) => new RankedShow
                {
                    Name = show.Name,
                    Rank = index + 1
                })));
        }

        [AllowAnonymous]
        [HttpGet("/")]
        [HttpGet("/index")]
        public async Task<ActionResult<PagedResult<RelativeRankedShow>>> GetAllShowsRelativelyRanked()
        {
            var page = HttpContext.Request.Query["page"].ToString();
            var pageNumber = string.IsNullOrEmpty(page) ? 1 : int.Parse(page);

            var pageSize = HttpContext.Request.Query["page-size"].ToString();
            var pageSizeNumber = string.IsNullOrEmpty(pageSize) ? 100 : int.Parse(pageSize);

            return await _repository.GetAllShowsRelativelyRanked(pageNumber, pageSizeNumber).ConfigureAwait(false);
        }

        [AllowAnonymous]
        [HttpGet("/show/all-shows")]
        public ActionResult<IEnumerable<Show>> GetAllShows() => Ok(_repository.GetAllShows());

        [AllowAnonymous]
        [HttpGet("/show/search")]
        public ActionResult<IEnumerable<Show>> Search([FromQuery(Name ="search-term")] string searchTerm)
        {
            return Ok(_repository.Search(searchTerm));
        }

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
