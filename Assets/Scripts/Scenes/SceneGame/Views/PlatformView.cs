using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class PlatformView : MonoBehaviour, IView
    {
        [SerializeField]
        private Transform platformBallStartPosition;
        private PlatformModel _platformModel;

        private float HalfScreen => Screen.width / 2f;

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
            var position = transform.position;
            
            if (_platformModel.Position.x < HalfScreen)
            {
                position.x -= _platformModel.PlatformSpeed;
            }
            else
            {
                position.x += _platformModel.PlatformSpeed;
            }

            transform.position = position;
        }
    }
}