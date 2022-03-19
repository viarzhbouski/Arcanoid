using Managers;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class BallController : IController, IHasUpdate
    {
        private readonly BallModel _ballModel;
        private readonly BallView _ballView;
        private readonly MainConfig _mainConfig;
        private readonly LifesController _lifesController;
        private bool _isHold;
        
        public BallController(IView view, LifesController lifesController, MainConfig mainConfig)
        {
            _lifesController = lifesController;
            _mainConfig = mainConfig;
            _ballModel = new BallModel();
            _ballView = view as BallView;
            _ballView!.Bind(_ballModel, this);
            _ballModel.OnChangeHandler(ControllerOnChange);
        }

        public void ControllerOnChange()
        {
            _ballView.RenderChanges();
        }

        public void BallOutOfGameField()
        {
            _lifesController.DecreaseLife();
            _ballModel.IsStarted = false;
            _ballModel.OnChange?.Invoke();
        }

        public void UpdateController()
        {
            if (_ballModel.IsStarted)
            {
                _ballModel.OnChange?.Invoke();
                return;
            }
            
            if (Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                //PopupManager.Instance.AAA();
                _isHold = true;
            }
            else if (_isHold)
            {
                _isHold = false;
                _ballModel.Speed = _mainConfig.BallSpeed;
                _ballModel.IsStarted = true;
            }
        }

        public void UpdateBallPosition(Vector2 ballPosition)
        {
            _ballModel.BallPosition = ballPosition;
            
            if (_ballModel.IsStarted)
            {
                return;
            }
            
            _ballModel.OnChange?.Invoke();
        }
    }
}