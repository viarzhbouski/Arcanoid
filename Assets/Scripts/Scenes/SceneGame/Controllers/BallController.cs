using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;
using Object = UnityEngine.Object;

namespace Scenes.SceneGame.Controllers
{
    public class BallController : IController, IHasStart, IHasUpdate
    {
        private readonly BallModel _ballModel;
        private readonly BallView _ballView;
        private readonly List<BallView> _captiveBalls;
        
        private LifesController _lifesController;
        private PlatformController _platformController;
        private bool _isHold;

        public BallController(IView view)
        {
            _captiveBalls = new List<BallView>();
            _ballModel = new BallModel();
            _ballView = view as BallView;
            _ballView!.Bind(_ballModel, this);
            _ballModel.OnChangeHandler(ControllerOnChange);
            _ballModel.MinBounceAngle = AppConfig.Instance.BallAndPlatform.MinBounceAngle;
            SetDefaultSpeed();
        }
        
        public void StartController()
        {
            _lifesController = AppControllers.Instance.GetController<LifesController>();
            _platformController = AppControllers.Instance.GetController<PlatformController>();
        }

        public void UpdateController()
        {
            _ballModel.IsStarted = _platformController.IsStarted();

            if (!_ballModel.BallCanMove)
            {
                _ballModel.BallPosition = _platformController.GetPlatformBallStartPosition();
            }

            _ballModel.OnChange?.Invoke();
        }

        public void SetDefaultSpeed()
        {
            _ballModel.BallCanDestroyAllBlocks = false;
            _ballModel.Speed = AppConfig.Instance.BallAndPlatform.BallSpeed;
            DestroyAllCaptiveBalls();
        }

        public void SetBallExtraSpeed(float speed)
        {
            _ballModel.ExtraSpeed = speed;
        }

        public void SetBallCanDestroyAllBlocks(bool state)
        {
            _ballModel.BallCanDestroyAllBlocks = state;
        }
        
        public void ControllerOnChange()
        {
            _ballView.RenderChanges();

            if (_captiveBalls.Any())
            {
                foreach (var ball in _captiveBalls)
                {
                    ball.RenderChanges();
                }
            }
        }
        
        public void BallOutOfGameField()
        {
            _lifesController.DecreaseLife();
            _platformController.IsStarted(false);
        }

        public void AddCaptiveBall(BallView captiveBall)
        {
            captiveBall.Bind(_ballModel, this);
            captiveBall.IsCaptive = true;
            _captiveBalls.Add(captiveBall);
        }

        public void RemoveCaptiveBall(BallView captiveBall) => _captiveBalls.Remove(captiveBall);

        private void DestroyAllCaptiveBalls()
        {
            if (!_captiveBalls.Any())
            {
                return;
            }

            foreach (var captiveBall in _captiveBalls)
            {
                Object.Destroy(captiveBall.gameObject);
            }
            
            _captiveBalls.Clear();
        }
    }
}