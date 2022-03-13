using Scripts.Core.ObjectPooling;
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