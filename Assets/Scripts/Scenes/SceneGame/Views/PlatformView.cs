using Core.Interfaces.MVC;
using Scenes.SceneGame.Models;
using UnityEngine;

namespace Scenes.SceneGame.Views
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
        private readonly float _clickPointAndPlatformMinDir = 0.25f;
        
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
                platformRigidbody2D.velocity = Vector2.zero;
                return;
            }
            
            var tapPosition = platformCamera.ScreenToWorldPoint(_platformModel.Position);
            var tapPositionX = new Vector3(tapPosition.x, Vector2.zero.y);
            var positionX = new Vector3(transform.position.x, Vector2.zero.y);
            var mouseDir = tapPositionX - positionX;

            if (mouseDir.magnitude <= _clickPointAndPlatformMinDir)
            {
                platformRigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                platformRigidbody2D.velocity = mouseDir.normalized * _platformModel.PlatformSpeed;
            }
        }
    }
}