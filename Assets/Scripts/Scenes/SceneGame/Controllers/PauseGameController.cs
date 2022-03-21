using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class PauseGameController : IController
    {
        private readonly PauseGameModel _pauseGameModel;
        private readonly PauseGameView _pauseGameView;
        private readonly BallController _ballController;
        private readonly MainConfig _mainConfig;

        public PauseGameController(IView view, BallController ballController, MainConfig mainConfig)
        {
            _ballController = ballController;
            _mainConfig = mainConfig;
            _pauseGameModel = new PauseGameModel();
            _pauseGameView = view as PauseGameView;
            _pauseGameView!.Bind(_pauseGameModel, this);
            _pauseGameModel.OnChangeHandler(ControllerOnChange);
        }

        public void ControllerOnChange()
        {
            _pauseGameView.RenderChanges();
        }

        public void GameInPause(bool stopBall)
        {
            _ballController.SetBallState(stopBall);
        }
    }
}