using System;
using System.Collections;
using System.Collections.Generic;
using Core.ObjectPooling.Interfaces;
using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.ScenePools;
using UnityEngine;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost
{
    public static class BonusesTimer
    {
        public static readonly Dictionary<Type, float> BonusTimeDict = new Dictionary<Type, float>();
    }
    
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
        private bool _isCatched;
        
        public void Init(IHasBonusBoost bonusBoost)
        {
            _bonusBoost = bonusBoost;
            bonusSpriteRenderer.color = bonusBoost.BonusColor;
            bonusRigidbody.AddForce(Vector2.down * AppConfig.Instance.BoostsConfig.BonusSpeed);
            bonusParticleSystem.gameObject.SetActive(true);
            bonusBoostCollider.enabled = true;
        }

        private void Update()
        {
            if (!TransformHelper.ObjectAtGamefield(transform.position))
            {
                AppObjectPools.Instance.GetObjectPool<BonusBoostPool>().DestroyPoolObject(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var platformView = collision.gameObject.GetComponent<PlatformView>();
            if (platformView)
            {
                _isCatched = true;
                var bonusType = _bonusBoost.GetType();
                SetBonusObjectInvisible();

                if (!BonusesTimer.BonusTimeDict.ContainsKey(bonusType))
                {
                    BonusesTimer.BonusTimeDict.Add(bonusType, _bonusBoost.BonusWorkingDelay);
                    StartCoroutine(ApplyBonusBoost());
                }
                else
                {
                    BonusesTimer.BonusTimeDict[_bonusBoost.GetType()] = _bonusBoost.BonusWorkingDelay;
                    AppObjectPools.Instance.GetObjectPool<BonusBoostPool>().DestroyPoolObject(this);
                }
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
            var bonusType = _bonusBoost.GetType();
            while (true)
            {
                yield return new WaitForSeconds(1);
                
                BonusesTimer.BonusTimeDict[bonusType] -= 1f;
                if (BonusesTimer.BonusTimeDict[bonusType] <= 0)
                {
                    _bonusBoost.CancelBonusBoost();
                    AppObjectPools.Instance.GetObjectPool<BonusBoostPool>().DestroyPoolObject(this);
                    BonusesTimer.BonusTimeDict.Remove(bonusType);
                    break;
                }
            }
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}