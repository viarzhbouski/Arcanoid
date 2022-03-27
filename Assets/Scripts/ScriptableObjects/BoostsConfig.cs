using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New BoostsConfig", menuName = "Create Boosts Config")]
    public class BoostsConfig : ScriptableObject
    {
        [Header("\tBOMB")]
        [Space]
        [SerializeField]
        private float bombExecuteDelay;
        
        [Header("\tCHAIN BOMB")]
        [Space]
        [SerializeField]
        private float chainBombExecuteDelay;

        [Header("\t BALL ACCELERATION")]
        [Space]
        [SerializeField]
        private float ballAccelerationSpeed;
        [SerializeField]
        private float ballAccelerationWorkingDelay;
        
        [Header("\tBALL SLOWDOWN")]
        [Space]
        [SerializeField]
        private float ballSlowdownSpeed;
        [SerializeField]
        private float ballSlowdownWorkingDelay;
        
        [Header("\tPLATFORM ACCELERATION")]
        [Space]
        [SerializeField]
        private float platformAccelerationSpeed;
        [SerializeField]
        private float platformAccelerationWorkingDelay;
        
        [Header("\tPLATFORM SLOWDOWN")]
        [Space]
        [SerializeField]
        private float platformSlowdownSpeed;
        [SerializeField]
        private float platformSlowdownWorkingDelay;
        
        [Header("\tPLATFORM SIZE ENCREASE")]
        [Space]
        [SerializeField]
        private float platformSizeEncrease;
        [SerializeField]
        private float platformSizeEncreaseWorkingDelay;
        
        [Header("\tPLATFORM SIZE DECREASE")]
        [Space]
        [SerializeField]
        private float platformSizeDecrease;
        [SerializeField]
        private float platformSizeDecreaseWorkingDelay;
        
        [Header("\tFURY BALL")]
        [Space]
        [SerializeField]
        private float furyBallWorkingDelay;

        public float BombExecuteDelay => bombExecuteDelay;
        
        public float ChainBombExecuteDelay => chainBombExecuteDelay;
        
        public float BallAccelerationSpeed => ballAccelerationSpeed;
        
        public float BallAccelerationWorkingDelay => ballAccelerationWorkingDelay;
        
        public float BallSlowdownSpeed => ballSlowdownSpeed;
        
        public float BallSlowdownWorkingDelay => ballSlowdownWorkingDelay;
        
        public float PlatformAccelerationSpeed => platformAccelerationSpeed;
        
        public float PlatformAccelerationWorkingDelay => platformAccelerationWorkingDelay;
        
        public float PlatformSlowdownSpeed => platformSlowdownSpeed;
        
        public float PlatformSlowdownWorkingDelay => platformSlowdownWorkingDelay;
        
        public float PlatformSizeEncrease => platformSizeEncrease;
        
        public float PlatformSizeEncreaseWorkingDelay => platformSizeEncreaseWorkingDelay;
        
        public float PlatformSizeDecrease => platformSizeDecrease;
        
        public float PlatformSizeDecreaseWorkingDelay => platformSizeDecreaseWorkingDelay;
        
        public float FuryBallWorkingDelay => furyBallWorkingDelay;
    }
}