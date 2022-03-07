using System.Collections.Generic;
using Newtonsoft.Json;

namespace Scripts.Core.Models
{
    public class LevelMap
    {
        public class Layer
        {
            [JsonProperty("data")] 
            public List<int> Data { get; set; } = new List<int>();
            
            [JsonProperty("height")]
            public int Height { get; set; }
            
            [JsonProperty("width")]
            public int Width { get; set; }
        }
        
        [JsonProperty("layers")]
        public List<Layer> Layers { get; set; } = new List<Layer>();
    }
}