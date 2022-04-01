using System.Collections;
using Core.ObjectPooling.Interfaces;
using Core.Statics;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.ScenePools;
using UnityEngine;

namespace Scenes.SceneGame.Views
{
    public class CaptiveBallView : MonoBehaviour, IPoolable
    {
        [SerializeField]
        private BallView ballView;

        private BallController _ballController;
        
        public void Init()
        {
            _ballController = AppControllers.Instance.GetController<BallController>();
            _ballController.AddCaptiveBall(ballView);
            StartCoroutine(DestroyCaptiveBall());
        }

        IEnumerator DestroyCaptiveBall()
        {
            yield return new WaitForSeconds(AppConfig.Instance.BoostsConfig.BallLifeTime);
            _ballController.RemoveCaptiveBall(ballView);
            _ballController = null;
            AppObjectPools.Instance.GetObjectPool<CaptiveBallPool>()
                            .DestroyPoolObject(this);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}