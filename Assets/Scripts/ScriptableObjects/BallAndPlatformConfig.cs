using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New BallAndPlatformConfig", menuName = "Create Ball And Platform Config")]
    public class BallAndPlatformConfig : ScriptableObject
    {
        [SerializeField]
        private float ballSpeed;
        [SerializeField]
        private float ballSpeedEncrease;
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
        [SerializeField]
        private Sprite ballSprite;
        [SerializeField]
        private Sprite furyBallSprite;
        [SerializeField]
        private Gradient ballTrail;
        [SerializeField]
        private Color ballColor;
        [SerializeField]
        private Gradient furyBallTrail;
        [SerializeField]
        private Color furyBallColor;
        [SerializeField]
        private Color platformNonCollisionColor;
        [SerializeField]
        private Color platformCollisionColor;
        
        public Sprite BallSprite => ballSprite;
        
        public Sprite FuryBallSprite => furyBallSprite;
        
        public Color BallColor => ballColor;
        
        public Color FuryBallColor => furyBallColor;
        
        public float BallSpeed => ballSpeed;
        
        public float BallSpeedEncrease => ballSpeedEncrease;
        
        public int BallDamage => ballDamage;
        
        public float MinBounceAngle => minBounceAngle;
        
        public int LifeCount => lifeCount;
        
        public int MaxLifeCount => maxLifeCount;
        
        public float PlatformSpeed => platformSpeed;
        
        public Gradient BallTrail => ballTrail;
        
        public Gradient FuryBallTrail => furyBallTrail;
        
        public Color PlatformNonCollisionColor => platformNonCollisionColor;
        
        public Color PlatformCollisionColor => platformCollisionColor;
    }
}