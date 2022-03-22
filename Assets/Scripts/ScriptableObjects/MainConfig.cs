using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "Create main config")]
    public class MainConfig : ScriptableObject
    {
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
        private Pack[] packs;
        
        [Header("\tBLOCKS")] 
        [Space]
        [SerializeField]
        private Block[] blocks;
        
        [Header("\tPOPUPS")] 
        [Space]
        [SerializeField]
        private float pausePopupDelayAfterContinue;

        public int LifeCount => lifeCount;
        
        public int MaxLifeCount => maxLifeCount;
        
        public float BallSpeed => ballSpeed;
        
        public float MinBounceAngle => minBounceAngle;
        
        public float PlatformSpeed => platformSpeed;
        
        public Pack[] Packs => packs;
        
        public Block[] Blocks => blocks;
        
        public float MaxViewportSize => maxViewportSize;

        public float SpaceWidth => spaceWidth;
        
        public float SpaceHeight => spaceHeight;
        
        public float PausePopupDelayAfterContinue => pausePopupDelayAfterContinue;
        
    }
}