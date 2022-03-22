using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class PauseGameController : IController
    {
        private readonly PauseGameModel _pauseGameModel;
        private readonly PauseGameView _pauseGameView;
        private readonly BallController _ballController;
        private readonly GenerateLevelController _generateLevelController;
        private readonly LifesController _lifesController;
        private readonly MainConfig _mainConfig;

        public PauseGameController(IView view, BallController ballController, GenerateLevelController generateLevelController, LifesController lifesController, MainConfig mainConfig)
        {
            _generateLevelController = generateLevelController;
            _ballController = ballController;
            _lifesController = lifesController;
            _mainConfig = mainConfig;
            _pauseGameModel = new PauseGameModel();
            _pauseGameView = view as PauseGameView;
            _pauseGameView!.Bind(_pauseGameModel, this);
            _pauseGameModel.OnChangeHandler(ControllerOnChange);
            _pauseGameModel.PausePopupDelayAfterContinue = mainConfig.PausePopupDelayAfterContinue;
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
        }
    }
}