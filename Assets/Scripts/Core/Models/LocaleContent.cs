using Common.Enums;
using Newtonsoft.Json;

namespace Core.Models
{
    public class LocaleContent
    {
        [JsonProperty("localeLanguage")] 
        public LocaleLanguages LocaleLanguage { get; set; }
            
        [JsonProperty("localeFieldContent")]
        public LocaleFieldContent[] LocaleFieldContents{ get; set; }
    }
}