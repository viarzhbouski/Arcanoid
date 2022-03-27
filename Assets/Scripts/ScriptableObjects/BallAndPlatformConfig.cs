using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New BallAndPlatformConfig", menuName = "Create Ball And Platform Config")]
    public class BallAndPlatformConfig : ScriptableObject
    {
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
        
        public float BallSpeed => ballSpeed;
        
        public float MinBounceAngle => minBounceAngle;
        
        public int LifeCount => lifeCount;
        
        public int MaxLifeCount => maxLifeCount;
        
        public float PlatformSpeed => platformSpeed;
    }
}