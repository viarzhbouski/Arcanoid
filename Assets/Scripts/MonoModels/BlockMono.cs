using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Scripts.ScriptableObjects;
using UnityEngine;
using DG.Tweening;

namespace MonoModels
{
    public class BlockMono : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        [SerializeField]
        private List<Sprite> damageSprites;
        
        private Queue<Sprite> _spriteQueue;
        private Block _block;
        private int _damageForChangeSprite;
        private int _damageSum;
        
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        public Block Block => _block;

        public bool CanDestroy => _block.BlockType != BlockTypes.Undestroyable && _block.HealthPoints <= 0;
        
        public void SetBlockConfig(Block block)
        {
            _block = block;
            spriteRenderer.color = block.Color;

            if (_block.HealthPoints > damageSprites.Count)
            {
                _damageForChangeSprite = _block.HealthPoints / damageSprites.Count;
            }
            else
            {
                _damageForChangeSprite = 1;
            }

            _spriteQueue = new Queue<Sprite>();
            _damageSum = 0;
            
            foreach (var damageSprite in damageSprites)
            {
                _spriteQueue.Enqueue(damageSprite);
            }
        }

        public void Damage()
        {
            _block.HealthPoints--;
            _damageSum++;
            
            PlayDamageAnim();
            ChangeSprite();
        }

        private void PlayDamageAnim()
        {
            transform.DOShakePosition(0.05f, 0.5f).SetEase(Ease.OutBounce);
        }

        private void ChangeSprite()
        {
            if (_block.BlockType != BlockTypes.Undestroyable &&_damageSum % _damageForChangeSprite == 0 && _spriteQueue.Any())
            {
                spriteRenderer.sprite = _spriteQueue.Dequeue();
            }
        }
        
    }
}