using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;

namespace Scenes.SceneGame.Controllers
{
    public class BallController : IController, IHasStart, IHasUpdate
    {
        private readonly BallModel _ballModel;
        private readonly BallView _ballView;

        private LifesController _lifesController;
        private PlatformController _platformController;
        private bool _isHold;

        public BallController(IView view)
        {
            _ballModel = new BallModel();
            _ballView = view as BallView;
            _ballView!.Bind(_ballModel, this);
            _ballModel.OnChangeHandler(ControllerOnChange);
            _ballModel.MinBounceAngle = AppConfig.Instance.BallAndPlatform.MinBounceAngle;
            _ballModel.Speed = AppConfig.Instance.BallAndPlatform.BallSpeed;
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
        }
        
        public void BallOutOfGameField()
        {
            _lifesController.DecreaseLife();
            _platformController.IsStarted(false);
        }
    }
}