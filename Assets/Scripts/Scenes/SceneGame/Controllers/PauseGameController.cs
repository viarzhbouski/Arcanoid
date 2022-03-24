using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class PauseGameController : IController, IHasStart
    {
        private readonly PauseGameModel _pauseGameModel;
        private readonly PauseGameView _pauseGameView;
        private readonly MainConfig _mainConfig;
        
        private BallController _ballController;
        private GenerateLevelController _generateLevelController;
        private LifesController _lifesController;

        public PauseGameController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _pauseGameModel = new PauseGameModel();
            _pauseGameView = view as PauseGameView;
            _pauseGameView!.Bind(_pauseGameModel, this);
            _pauseGameModel.OnChangeHandler(ControllerOnChange);
            _pauseGameModel.PausePopupDelayAfterContinue = mainConfig.PausePopupDelayAfterContinue;
        }
        
        public void StartController()
        {
            _generateLevelController = AppContext.Context.GetController<GenerateLevelController>();
            _ballController = AppContext.Context.GetController<BallController>();
            _lifesController = AppContext.Context.GetController<LifesController>();
        }

        public void ControllerOnChange()
        {
            _pauseGameView.RenderChanges();
        }

        public void GameInPause(bool stopBall)
        {
            _ballController.SetBallState(stopBall);
        }

        public void RestartLevel()
        {
            _generateLevelController.ReloadLevel();
            _ballController.ReloadBallForNewGame();
            _lifesController.LoadLifes();
            GameInPause(false);
        }
    }
}