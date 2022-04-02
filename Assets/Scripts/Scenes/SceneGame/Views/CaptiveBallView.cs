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

        public BallView BallView => ballView;
        
        public void Init()
        {
            _ballController = AppControllers.Instance.GetController<BallController>();
            _ballController.AddCaptiveBall(this);
            StartCoroutine(DestroyCaptiveBall());
        }

        IEnumerator DestroyCaptiveBall()
        {
            yield return new WaitForSeconds(AppConfig.Instance.BoostsConfig.BallLifeTime);
            _ballController.RemoveCaptiveBall(this);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}