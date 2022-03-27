using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New BallAndPlatformConfig", menuName = "Create Ball And Platform Config")]
    public class BallAndPlatformConfig : ScriptableObject
    {
        [SerializeField]
        private float ballSpeed;
        [SerializeField]
        private int ballDamage;
        [SerializeField]
        private float minBounceAngle;
        [SerializeField]
        private float platformSpeed;
        [SerializeField]
        private int lifeCount;
        [SerializeField]
        private int maxLifeCount;
        
        public float BallSpeed => ballSpeed;
        
        public int BallDamage => ballDamage;
        
        public float MinBounceAngle => minBounceAngle;
        
        public int LifeCount => lifeCount;
        
        public int MaxLifeCount => maxLifeCount;
        
        public float PlatformSpeed => platformSpeed;
    }
}