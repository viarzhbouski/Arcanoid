using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class PlatformView : MonoBehaviour, IView
    {
        [SerializeField]
        private Rigidbody2D platformRigidbody2D;
        [SerializeField]
        private Camera platformCamera;
        [SerializeField]
        private Transform platformBallStartPosition;
        
        private PlatformModel _platformModel;
        private Vector2? _prevPosition;
        
        public void Bind(IModel model, IController controller)
        {
            _platformModel = model as PlatformModel;
            _platformModel!.Position = transform.position;
            SetPlatformBallStartPosition();
        }
        
        public void RenderChanges()
        {
            SetPlatformPosition();
            SetPlatformBallStartPosition();
        }
        
        private void SetPlatformBallStartPosition() => _platformModel.PlatformBallStartPosition = platformBallStartPosition.position;

        private void SetPlatformPosition()
        {
            if (!_platformModel.IsHold)
            {
                return;
            }
            
            var tapPosition = platformCamera.ScreenToWorldPoint(_platformModel.Position);
            var tapPositionX = new Vector2(tapPosition.x, Vector2.zero.y);
            
            platformRigidbody2D.AddForce(tapPositionX * _platformModel.PlatformSpeed);
        }
    }
}