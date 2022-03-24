using System;
using Managers;
using Scenes.SceneGame.Views.Popups;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;
using UnityEngine;
using AppContext = Scripts.Core.AppContext;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class BallController : IController, IHasStart, IHasUpdate
    {
        private readonly BallModel _ballModel;
        private readonly BallView _ballView;
        private readonly MainConfig _mainConfig;
        
        private LifesController _lifesController;
        private LevelProgressController _levelProgressController;
        private bool _isHold;

        public BallController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _ballModel = new BallModel();
            _ballView = view as BallView;
            _ballView!.Bind(_ballModel, this);
            _ballModel.MinBounceAngle = mainConfig.MinBounceAngle;
            _ballModel.OnChangeHandler(ControllerOnChange);
        }
        
        public void StartController()
        {
            _lifesController = AppContext.Context.GetController<LifesController>();
            _levelProgressController = AppContext.Context.GetController<LevelProgressController>();
        }

        public void ControllerOnChange()
        {
            _ballView.RenderChanges();
        }

        public void BallOutOfGameField()
        {
            _lifesController.DecreaseLife();
            ReloadBall();
        }

        public void ReloadBall()
        {
            _ballModel.IsStarted = false;
            _ballModel.OnChange?.Invoke();
        }

        public void ReloadBallForNewGame()
        {
            _levelProgressController.InitProgressBar();
            ReloadBall();
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
            if (_ballModel.IsStarted)
            {
                return;
            }
            
            _ballModel.BallPosition = ballPosition;
            _ballModel.OnChange?.Invoke();
        }

        public void SetBallState(bool isStopped)
        {
            _ballModel.BallIsStopped = isStopped;
        }
    }
}