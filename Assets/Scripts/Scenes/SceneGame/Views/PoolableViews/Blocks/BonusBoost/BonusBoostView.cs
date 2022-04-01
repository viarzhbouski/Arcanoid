using System.Collections;
using Core.ObjectPooling.Interfaces;
using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.ScenePools;
using UnityEngine;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost
{
    public class BonusBoostView : MonoBehaviour, IPoolable
    {
        [SerializeField]
        private CapsuleCollider2D bonusBoostCollider;
        [SerializeField]
        private SpriteRenderer bonusSpriteRenderer;
        [SerializeField]
        private ParticleSystem bonusParticleSystem;
        [SerializeField] 
        private Rigidbody2D bonusRigidbody;
        
        private IHasBonusBoost _bonusBoost;
        
        public void Init(IHasBonusBoost bonusBoost)
        {
            _bonusBoost = bonusBoost;
            bonusSpriteRenderer.color = bonusBoost.BonusColor;
            bonusRigidbody.AddForce(Vector2.down * AppConfig.Instance.BoostsConfig.BonusSpeed);
            bonusParticleSystem.gameObject.SetActive(true);
            bonusBoostCollider.enabled = true;
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
            bonusParticleSystem.gameObject.SetActive(false);
        }

        IEnumerator ApplyBonusBoost()
        {
            _bonusBoost.ApplyBonusBoost();
            yield return new WaitForSeconds(_bonusBoost.BonusWorkingDelay);
            _bonusBoost.CancelBonusBoost();
            AppObjectPools.Instance.GetObjectPool<BonusBoostPool>().DestroyPoolObject(this);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}