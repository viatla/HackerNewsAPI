using HackerNewsAPI.Models;
using HackerNewsAPI.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
        private readonly NewsService _newsService;
        private const string DATETIME_FORMAT = "yyyy-MM-ddTHH:mm:sszzz";

        public StoriesController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("{n}")]
        public async Task<IActionResult> GetBestStories(int n)
        {
            if (_log.IsDebugEnabled) _log.Debug($"GetBestStories({n}) -- Start");
            if (n <= 0)
            {
                return BadRequest("Invalid value for 'n'.");
            }
            
            var bestStoryIds = await _newsService.GetBestStoryIds();

            var bestNStoriesTasks = bestStoryIds
                        .Take(n)
                        .Select(async s => await _newsService.GetStory(s));                        

            var bestNStories = await Task.WhenAll(bestNStoriesTasks);

            var bestNNewsStories = bestNStories
                        .OrderByDescending(d => d.Score)
                        .Select(s => new NewsStory { Title = s.Title, Uri = s.Url, PostedBy = s.By, CommentCount = s.Descendants, Score = s.Score, Time = DateTime.UnixEpoch.AddSeconds(s.Time).ToString(DATETIME_FORMAT) })
                        .ToList();

            var s = _newsService.GetStory(21233041).Result;
            var t = new NewsStory { Title = s.Title, Uri = s.Url, PostedBy = s.By, CommentCount = s.Descendants, Score = s.Score, Time = DateTime.UnixEpoch.AddSeconds(s.Time).ToString(DATETIME_FORMAT) };
            bestNNewsStories.Add(t);

            if (_log.IsDebugEnabled) _log.Debug($"GetBestStories({n}) -- End");
            return Ok(bestNNewsStories);
        }
    }
}
