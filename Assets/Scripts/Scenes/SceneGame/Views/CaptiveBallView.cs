using System.Collections;
using Core.Statics;
using Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scenes.SceneGame.Views
{
    public class CaptiveBallView : MonoBehaviour
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
            Destroy(gameObject);
        }
    }
}