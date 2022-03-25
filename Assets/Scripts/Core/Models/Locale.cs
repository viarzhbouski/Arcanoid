using Newtonsoft.Json;

namespace Core.Models
{
    public class Locale
    {
        [JsonProperty("localeContents")]
        public LocaleContent[] LocaleContents{ get; set; }
    }
}