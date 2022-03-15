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
            SetPlatformBallStartPosition();
            SetPlatformPosition();
        }
        
        private void SetPlatformBallStartPosition() => _platformModel.PlatformBallStartPosition = platformBallStartPosition.position;

        private void SetPlatformPosition()
        {
            if (!_platformModel.IsHold)
            {
                platformRigidbody2D.velocity = Vector2.zero;
                return;
            }
            
            var tapPosition = platformCamera.ScreenToWorldPoint(_platformModel.Position);
            var tapPositionX = new Vector2(tapPosition.x, Vector2.zero.y);
            var platformPositionX = new Vector2(transform.position.x, Vector2.zero.y);
            var distance = Vector2.Distance(tapPositionX, platformPositionX);
            
            if (distance < _platformModel.PlatformStopDistance)
            {
                platformRigidbody2D.velocity = Vector2.zero;
            }
 
            if (_prevPosition.HasValue)
            {
                var directionValue = tapPosition.x - _prevPosition!.Value.x;
                
                if (directionValue < -_platformModel.PlatformMoveCoef)
                {
                    MovePlatform(Vector2.left);
                }
                else if (directionValue > _platformModel.PlatformMoveCoef)
                {
                    MovePlatform(Vector2.right);
                }
            }

            _prevPosition = tapPosition;

            void MovePlatform(Vector2 direction)
            {
                platformRigidbody2D.velocity = Vector2.zero;
                platformRigidbody2D.AddForce(direction * _platformModel.PlatformSpeed, ForceMode2D.Impulse);
            }
        }
    }
}