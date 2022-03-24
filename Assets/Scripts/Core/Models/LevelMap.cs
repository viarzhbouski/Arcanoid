using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Models
{
    public class Layer
    {
        [JsonProperty("data")] 
        public List<int> Data { get; set; } = new List<int>();
    }
    public class LevelMap
    {
        [JsonProperty("layers")] 
        public List<Layer> Layers { get; set; } = new List<Layer>();
            
        [JsonProperty("height")]
        public int Height { get; set; }
            
        [JsonProperty("width")]
        public int Width { get; set; }
        
        [JsonProperty("tileheight")]
        public int TileHeight { get; set; }
            
        [JsonProperty("tilewidth")]
        public int TileWidth { get; set; }
    }
}