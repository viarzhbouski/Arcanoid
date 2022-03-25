using Common.Enums;
using Newtonsoft.Json;

namespace Core.Models
{
    public class LocaleFieldContent
    {
        [JsonProperty("localeField")] 
        public LocaleFields LocaleField { get; set; }
            
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}