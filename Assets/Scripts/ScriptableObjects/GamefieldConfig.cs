using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New GamefieldConfig", menuName = "Create Gamefield Config")]
    public class GamefieldConfig : ScriptableObject
    {
        [Range(0.1f, 1f)]
        [SerializeField]
        private float maxViewportSize = 1f;
        [SerializeField]
        private float spaceWidth;
        [SerializeField]
        private float spaceHeight;
        
        public float MaxViewportSize => maxViewportSize;

        public float SpaceWidth => spaceWidth;
        
        public float SpaceHeight => spaceHeight;
    }
}