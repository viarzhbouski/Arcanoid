using System.Collections.Generic;
using Newtonsoft.Json;

namespace Scripts.Core.Models
{
    public class LevelMap
    {
        [JsonProperty("blocks")] 
        public List<int> Blocks { get; set; } = new List<int>();
            
        [JsonProperty("height")]
        public int Height { get; set; }
            
        [JsonProperty("width")]
        public int Width { get; set; }
    }
}