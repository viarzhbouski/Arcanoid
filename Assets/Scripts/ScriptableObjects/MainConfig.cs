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
        private int lifeCount;
        
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
        
        [Header("\tBlocks")] 
        [Space]
        [SerializeField]
        private Block[] blocks;

        public int LifeCount => lifeCount;
        
        public float BallSpeed => ballSpeed;
        
        public Pack[] Packs => packs;
        
        public Block[] Blocks => blocks;
        
        public float MaxViewportSize => maxViewportSize;

        public float SpaceWidth => spaceWidth;
        
        public float SpaceHeight => spaceHeight;
        
    }
}