using System.IO;
using Newtonsoft.Json;
using Scripts.Core.Models;

namespace Scripts.Services
{
    public class MapConvertService
    {
        public static LevelMap MapConvert()
        {
            var json = File.ReadAllText("./Assets/Levels/test.json");
            var a = JsonConvert.DeserializeObject<LevelMap>(json);
            return a;
        }
    }
}