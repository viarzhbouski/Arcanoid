using System.Collections.Generic;
using ScriptableObjects;
using ScriptableObjects.BlockConfigs;

namespace Core.Statics
{
    public class AppConfig
    {
        public static AppConfig Instance;
        
        private readonly MainConfig _mainConfig;
        
        public List<LocalizationConfig> Localizations => _mainConfig.LocalizationConfigs;

        public BallAndPlatformConfig BallAndPlatform => _mainConfig.BallAndPlatformConfig;
        
        public GamefieldConfig Gamefield => _mainConfig.GamefieldConfig;

        public List<PackConfig> Packs => _mainConfig.Packs;
        
        public List<BaseBlockConfig> Blocks => _mainConfig.Blocks;
        
        public PopupsConfig PopupsConfig => _mainConfig.PopupsConfig;
        
        public BoostsConfig BoostsConfig => _mainConfig.BoostsConfig;
        
        public EnergyConfig EnergyConfig => _mainConfig.EnergyConfig;

        public AppConfig(MainConfig mainConfig)
        {
            if (Instance == null)
            {
                Instance = this;
                _mainConfig = mainConfig;
            }
        }
    }
}