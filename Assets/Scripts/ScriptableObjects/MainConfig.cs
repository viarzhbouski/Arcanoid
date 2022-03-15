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
        private float platformSpeed;
        [SerializeField]
        private int lifeCount;
        
        [Header("\tGRID")] 
        [Space]
        [SerializeField]
        private float width;
        [SerializeField]
        private float height;
        [SerializeField]
        private float maxWidth;
        [SerializeField]
        private float maxHeight;
        [SerializeField]
        private float spaceWidth;
        [SerializeField]
        private float spaceHeight;
 
        [Header("\tPACKS")] 
        [Space]
        [SerializeField]
        private Pack[] packs;

        public int LifeCount => lifeCount;
        
        public float BallSpeed => ballSpeed;
        
        public float PlatformSpeed => platformSpeed;
        
        public Pack[] Packs => packs;
        
        public float Width => width;
        
        public float Height => height;
        
        public float MaxWidth => maxWidth;
        
        public float MaxHeight => maxHeight;
        
        public float SpaceWidth => spaceWidth;
        
        public float SpaceHeight => spaceHeight;
        
    }
}