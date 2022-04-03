using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views;
using UnityEngine;

namespace Scenes.SceneGame.Controllers
{
    public class BallController : IController, IHasStart, IHasUpdate
    {
        private readonly BallModel _ballModel;
        private readonly BallView _ballView;
        private readonly List<CaptiveBallView> _captiveBalls;
        
        private LifesController _lifesController;
        private PlatformController _platformController;
        private bool _isHold;

        public BallController(IView view)
        {
            _captiveBalls = new List<CaptiveBallView>();
            _ballModel = new BallModel();
            _ballView = view as BallView;
            _ballView!.Bind(_ballModel, this);
            _ballModel.OnChangeHandler(ControllerOnChange);
            _ballModel.MinBounceAngle = AppConfig.Instance.BallAndPlatform.MinBounceAngle;
            SetDefaultBallParams();
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

        public void SetDefaultBallParams()
        {
            _ballModel.BallCanDestroyAllBlocks = false;
            _ballModel.Speed = AppConfig.Instance.BallAndPlatform.BallSpeed;
            _ballModel.ExtraSpeed = 0;
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
                for (int i = 0; i < _captiveBalls.Count; i++)
                {
                    _captiveBalls[i].BallView.RenderChanges();
                }
            }
        }
        
        public void BallOutOfGameField()
        {
            _lifesController.DecreaseLife();
            _platformController.IsStarted(false);
        }

        public void AddCaptiveBall(CaptiveBallView captiveBall)
        {
            captiveBall.BallView.Bind(_ballModel, this);
            captiveBall.BallView.IsCaptive = true;
            _captiveBalls.Add(captiveBall);
        }

        public void RemoveCaptiveBall(CaptiveBallView captiveBall)
        {
            if (_captiveBalls.Contains(captiveBall))
            {
                AppObjectPools.Instance.GetObjectPool<CaptiveBallPool>()
                    .DestroyPoolObject(captiveBall);
                _captiveBalls.Remove(captiveBall);
            }
        }

        private void DestroyAllCaptiveBalls()
        {
            if (!_captiveBalls.Any())
            {
                return;
            }

            foreach (var captiveBall in _captiveBalls)
            {
                AppObjectPools.Instance.GetObjectPool<CaptiveBallPool>()
                               .DestroyPoolObject(captiveBall);
            }
            
            _captiveBalls.Clear();
        }
    }
}