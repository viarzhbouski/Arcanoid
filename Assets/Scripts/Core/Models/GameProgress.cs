using Newtonsoft.Json;

namespace Core.Models
{
    public class GameProgress
    {
        [JsonProperty("currentPack")]
        public int CurrentPack { get; set; }
        
        [JsonProperty("currentLevel")]
        public int CurrentLevel { get; set; }
    }
}