using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;
using ScriptableObjects;
using UnityEngine;

namespace Scenes.SceneGame.Controllers
{
    public class PlatformController : IController, IHasStart, IHasUpdate
    {
        private readonly PlatformModel _platformModel;
        private readonly PlatformView _platformView;
        private readonly MainConfig _mainConfig;
        
        private BallController _ballController;

        public PlatformController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _platformModel = new PlatformModel();
            _platformView = view as PlatformView;
            
            _platformView!.Bind(_platformModel, this);
            _platformModel.PlatformSpeed = mainConfig.BallSpeed;
            _platformModel.OnChangeHandler(ControllerOnChange);
        }
        
        public void StartController()
        {
            _ballController = AppControllers.Instance.GetController<BallController>();
        }
        
        public void UpdateController()
        {
            Move();
        }
        
        private void Move()
        {
            if (Input.touchCount > 0)
            {
                SetInputPosition(Input.GetTouch(0).position);
            }
            
            else if (Input.GetMouseButton(0))
            {
                SetInputPosition(Input.mousePosition);
            }
            else
            {
                _platformModel.IsHold = false;
            }
            
            _ballController.UpdateBallPosition(_platformModel.PlatformBallStartPosition);
            _platformModel.OnChange?.Invoke();
        }

        private void SetInputPosition(Vector2 inputPosition)
        {
            _platformModel.IsHold = true;
            _platformModel.Position = inputPosition;
        }

        public void ControllerOnChange()
        {
            _platformView.RenderChanges();
        }
    }
}
