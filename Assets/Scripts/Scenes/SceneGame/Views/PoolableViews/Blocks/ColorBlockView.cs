using System.Collections.Generic;
using System.Linq;
using Boosts.Interfaces;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scenes.SceneGame.Views.Blocks
{
    public class ColorBlockView : BaseBlockView
    {
        [SerializeField]
        private List<Sprite> damageSprites;
        
        private Queue<Sprite> _spriteQueue;
        private int _damageForChangeSprite;
        private int _damageSum;

        public override void SetBlockConfig(Block block)
        {
            base.SetBlockConfig(block);

            if (Block.HealthPoints > damageSprites.Count)
            {
                _damageForChangeSprite = Block.HealthPoints / damageSprites.Count;
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

            blockSpriteRenderer.sprite = _spriteQueue.Dequeue();
        }

        public override void SetBoost(IHasBoost boost)
        {
            return;
        }

        public override void BlockHit()
        {
            Block.HealthPoints--;
            _damageSum++;
            
            PlayBlockHitAnim();
            ChangeSprite();
        }

        private void ChangeSprite()
        {
            if (_damageSum % _damageForChangeSprite == 0 && _spriteQueue.Any())
            {
                blockSpriteRenderer.sprite = _spriteQueue.Dequeue();
            }
        }
    }
}