using UnityEngine;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class PlatformController : IController, IHasUpdate
    {
        private readonly BallController _ballController;
        private readonly PlatformModel _platformModel;
        private readonly PlatformView _platformView;
        private readonly MainConfig _mainConfig;

        public PlatformController(IView view, BallController ballController, MainConfig mainConfig)
        {
            _ballController = ballController;
            _mainConfig = mainConfig;
            _platformModel = new PlatformModel();
            _platformView = view as PlatformView;
            
            _platformView!.Bind(_platformModel, this);
            _platformModel.OnChangeHandler(ControllerOnChange);
            _platformModel.PlatformSpeed = mainConfig.PlatformSpeed;
        }
        
        public void UpdateController()
        {
            Move();
        }
        
        private void Move()
        {
            var isHold = false;
            
            if (Input.touchCount > 0)
            {
                isHold = true;
                var touch = Input.GetTouch(0);
                _platformModel.Position = touch.position;
            }
            else if (Input.GetMouseButton(0))
            {
                isHold = true;
                _platformModel.Position =Input.mousePosition;;
            }

            if (isHold)
            {
                isHold = false;
                _platformModel.OnChange?.Invoke();
                _ballController.UpdateBallPosition(_platformModel.PlatformBallStartPosition);
            }
        }

        public void ControllerOnChange()
        {
            _platformView.RenderChanges();
        }
    }
}
