using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "Create main config")]
    public class MainConfig : ScriptableObject
    {
        [Header("\tLocalization")] 
        [Space]
        [SerializeField]
        private List<LocalizationConfig> localizationConfigs;
        
        [Header("\tBALL AND PLATFORM")] 
        [Space]
        [SerializeField]
        private float ballSpeed;
        [SerializeField]
        private float minBounceAngle;
        [SerializeField]
        private float platformSpeed;
        [SerializeField]
        private int lifeCount;
        [SerializeField]
        private int maxLifeCount;
        
        [Header("\tGRID")] 
        [Space]
        [Range(0.1f, 1f)]
        [SerializeField]
        private float maxViewportSize = 1f;
        [SerializeField]
        private float spaceWidth;
        [SerializeField]
        private float spaceHeight;
 
        [Header("\tPACKS")] 
        [Space]
        [SerializeField]
        private List<PackConfig> packs;
        
        [Header("\tBLOCKS")] 
        [Space]
        [SerializeField]
        private Block[] blocks;

        [Header("\tPOPUPS")] 
        [Space]
        [SerializeField]
        private float pausePopupDelayAfterContinue;

        public List<LocalizationConfig> LocalizationConfigs => localizationConfigs;
        
        public int LifeCount => lifeCount;
        
        public int MaxLifeCount => maxLifeCount;
        
        public float BallSpeed => ballSpeed;
        
        public float MinBounceAngle => minBounceAngle;
        
        public float PlatformSpeed => platformSpeed;
        
        public List<PackConfig> Packs => packs;
        
        public Block[] Blocks => blocks;
        
        public float MaxViewportSize => maxViewportSize;

        public float SpaceWidth => spaceWidth;
        
        public float SpaceHeight => spaceHeight;
        
        public float PausePopupDelayAfterContinue => pausePopupDelayAfterContinue;
        
    }
}