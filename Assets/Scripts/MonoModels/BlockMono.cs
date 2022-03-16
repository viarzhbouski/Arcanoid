using Common.Enums;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace MonoModels
{
    public class BlockMono : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        private Block _block;

        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        public Block Block => _block;

        public bool CanDestroy =>  _block.BlockType != BlockTypes.Undestroyable && _block.HealthPoints <= 0;

        public void SetBlockConfig(Block block)
        {
            _block = block;
            spriteRenderer.color = block.Color;
        }

        public void Damage()
        {
            _block.HealthPoints--;
        }
    }
}