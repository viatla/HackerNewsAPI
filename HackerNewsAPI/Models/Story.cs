using System.Text.Json.Serialization;

namespace HackerNewsAPI.Models
{
    /** Example
     * 
     * {"by":"anticorporate",
     * "descendants":14,
     * "id":38519012,
     * "kids":[38519228,38521881,38522042],
     * "score":1158,
     * "time":1701706198,
     * "title":"Harvard gutted team examining Facebook Files following $500M Zuckerberg donation",
     * "type":"story",
     * "url":"https://live-whistleblower-aid.pantheonsite.io/joan-donovan-press-release/"}
     */
    public class Story
    {
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("url")]
        public string Url { get; set; }
        
        [JsonPropertyName("by")]
        public string By { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("descendants")]
        public int Descendants { get; set; }

    }
}
