using System.Collections.Generic;
using ScriptableObjects.BlockConfigs;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New MainConfig", menuName = "Create Main Config")]
    public class MainConfig : ScriptableObject
    {
        [Header("\tLOCALIZATION")] 
        [Space]
        [SerializeField]
        private List<LocalizationConfig> localizationConfigs;

        [Header("\tBALL AND PLATFORM")] 
        [Space]
        [SerializeField]
        private BallAndPlatformConfig ballAndPlatformConfig;

        [Header("\tGAMEFIELD")] 
        [Space]
        [SerializeField]
        private GamefieldConfig gamefieldConfig;
 
        [Header("\tPACKS")] 
        [Space]
        [SerializeField]
        private List<PackConfig> packs;
        
        [Header("\tBLOCKS")] 
        [Space]
        [SerializeField]
        private List<BaseBlockConfig> blocks;

        [Header("\tPOPUPS")] 
        [Space]
        [SerializeField]
        public PopupsConfig popupsConfig;
        
        [Header("\tBOOSTS")] 
        [Space]
        [SerializeField]
        public BoostsConfig boostsConfig;

        public List<LocalizationConfig> LocalizationConfigs => localizationConfigs;

        public BallAndPlatformConfig BallAndPlatformConfig => ballAndPlatformConfig;
        
        public GamefieldConfig GamefieldConfig => gamefieldConfig;

        public List<PackConfig> Packs => packs;
        
        public List<BaseBlockConfig> Blocks => blocks;
        
        public PopupsConfig PopupsConfig => popupsConfig;
        
        public BoostsConfig BoostsConfig => boostsConfig;
    }
}