using ScriptableObjects;

namespace Core.Statics
{
    public class AppConfig
    {
        public static AppConfig Instance;
        public MainConfig Config { get; }

        public AppConfig(MainConfig mainConfig)
        {
            if (Instance == null)
            {
                Instance = this;
                Config = mainConfig;
            }
        }
    }
}