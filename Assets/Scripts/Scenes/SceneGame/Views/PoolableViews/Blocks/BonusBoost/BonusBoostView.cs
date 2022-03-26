using System.Collections;
using Scenes.SceneGame.Boosts.Interfaces;
using UnityEngine;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost
{
    public class BonusBoostView : MonoBehaviour
    {
        [SerializeField]
        private CapsuleCollider2D bonusBoostCollider;
        [SerializeField]
        private SpriteRenderer bonusSpriteRenderer;
        
        private IHasBonusBoost _bonusBoost;
        
        public void Init(IHasBonusBoost bonusBoost)
        {
            _bonusBoost = bonusBoost;
            bonusSpriteRenderer.color = bonusBoost.BonusColor;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var platformView = collision.gameObject.GetComponent<PlatformView>();
            if (platformView)
            {
                SetBonusObjectInvisible();
                StartCoroutine(ApplyBonusBoost());
            }
        }

        private void SetBonusObjectInvisible()
        {
            bonusBoostCollider.enabled = false;
            bonusSpriteRenderer.color = new Color();
        }

        IEnumerator ApplyBonusBoost()
        {
            _bonusBoost.ApplyBonusBoost();
            yield return new WaitForSeconds(_bonusBoost.BonusWorkingDelay);
            _bonusBoost.CancelBonusBoost();
            Destroy(gameObject);
        }
    }
}