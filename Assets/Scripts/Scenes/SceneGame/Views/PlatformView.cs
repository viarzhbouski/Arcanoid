using DG.Tweening;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class PlatformView : MonoBehaviour, IView
    {
        [SerializeField]
        private Camera camera;
        [SerializeField]
        private Transform platformBallStartPosition;
        private PlatformModel _platformModel;
        
        public void Bind(IModel model, IController controller)
        {
            _platformModel = model as PlatformModel;
            _platformModel!.Position = transform.position;
            SetPlatformBallStartPosition();
        }
        
        public void RenderChanges()
        {
            SetPlatformBallStartPosition();
            SetPlatformPosition();
        }
        
        private void SetPlatformBallStartPosition() => _platformModel.PlatformBallStartPosition = platformBallStartPosition.position;

        private void SetPlatformPosition()
        {
            var tapPosition = camera.ScreenToWorldPoint(_platformModel.Position);
            transform.DOMoveX(tapPosition.x, _platformModel.PlatformSpeed);
        }
    }
}