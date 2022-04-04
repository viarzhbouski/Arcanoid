using System;
using System.Collections.Generic;
using Common.Enums;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Models
{
    public class LevelMapAttribute
    {
        [JsonProperty("indexes")] 
        public List<int> Indexes { get; set; } = new List<int>();

        [JsonProperty("attribute_color")] 
        public List<float> ColorRGB { get; set; } = new List<float>();
        
        [JsonProperty("attribute_boost_name")]
        public string BoostName { get; set; }
        
        public Color BlockColor
        {
            get
            {
                if (ColorRGB.Count == 3)
                {
                    return new Color(ColorRGB[0], ColorRGB[1], ColorRGB[2]);
                }
                
                return Color.white;
            }
        }

        public BoostTypes? BoostType
        {
            get
            {
                if (string.IsNullOrEmpty(BoostName))
                {
                    return null;
                }
            
                var values = Enum.GetValues(typeof(BoostTypes));

                foreach (var value in values)
                {
                    var name = Enum.GetName(typeof(BoostTypes), value);

                    if (name == BoostName)
                    {
                        return (BoostTypes)value;
                    }
                }

                return null;
            }
        }
    }
    
    public class LevelMap
    {
        [JsonProperty("level_map")] 
        public List<int> LevelMapData { get; set; } = new List<int>();
        
        [JsonProperty("level_map_properties")] 
        public List<LevelMapAttribute> LevelMapProperties { get; set; } = new List<LevelMapAttribute>();
            
        [JsonProperty("columns")]
        public int Columns { get; set; }
            
        [JsonProperty("rows")]
        public int Rows { get; set; }
    }
}