using HackerNewsAPI.Models;
using log4net;
using System.Text.Json;

namespace HackerNewsAPI.Services
{
    public class NewsService
    {

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
        private const string BASE_URL = "https://hacker-news.firebaseio.com/v0/"; //FIXME: This could be in config file
        private readonly HttpClient _httpClient;

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Story> GetStory(int storyId)
        {
            if (_log.IsDebugEnabled) _log.Debug($"GetStory({storyId}) -- Start");
            var json = await _httpClient.GetStringAsync($"{BASE_URL}item/{storyId}.json");
            if (_log.IsDebugEnabled) _log.Debug($"GetStory({storyId}) -- End");
            return JsonSerializer.Deserialize<Story>(json); 
        }

        public async Task<List<int>> GetBestStoryIds()
        {
            if (_log.IsDebugEnabled) _log.Debug("GetBestStoryIds() -- Start");
            var json = await _httpClient.GetStringAsync($"{BASE_URL}beststories.json");
            if (_log.IsDebugEnabled) _log.Debug($"GetBestStoryIds() -- End");
            return JsonSerializer.Deserialize<List<int>>(json); ;
        }
    }
}
