using UnityEngine;

namespace MonoModels
{
    public class BlockMono : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        public SpriteRenderer SpriteRenderer
        {
            get => spriteRenderer;
        }
    }
}