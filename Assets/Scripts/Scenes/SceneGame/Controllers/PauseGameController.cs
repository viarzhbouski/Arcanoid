﻿using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.ObjectPooling;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views;

namespace Scenes.SceneGame.Controllers
{
    public class PauseGameController : IController, IHasStart
    {
        private readonly PauseGameModel _pauseGameModel;
        private readonly PauseGameView _pauseGameView;
        
        private BallController _ballController;
        private GenerateLevelController _generateLevelController;
        private LifesController _lifesController;
        private LevelProgressController _levelProgressController;
        private PlatformController _platformController;

        public PauseGameController(IView view)
        {
            _pauseGameModel = new PauseGameModel();
            _pauseGameView = view as PauseGameView;
            _pauseGameView!.Bind(_pauseGameModel, this);
            _pauseGameModel.OnChangeHandler(ControllerOnChange);
            _pauseGameModel.PausePopupDelayAfterContinue = AppConfig.Instance.PopupsConfig.PausePopupDelayAfterContinue;
        }
        
        public void StartController()
        {
            _generateLevelController = AppControllers.Instance.GetController<GenerateLevelController>();
            _ballController = AppControllers.Instance.GetController<BallController>();
            _lifesController = AppControllers.Instance.GetController<LifesController>();
            _levelProgressController = AppControllers.Instance.GetController<LevelProgressController>();
            _platformController = AppControllers.Instance.GetController<PlatformController>();
        }

        public void ControllerOnChange()
        {
            _pauseGameView.RenderChanges();
        }

        public void GameInPause(bool stopBall)
        {
            _ballController.StopBall(stopBall);
        }
        
        private void ClearBlockPools()
        {
            ObjectPools.Instance.GetObjectPool<ColorBlockPool>()
                .ClearPool();
            ObjectPools.Instance.GetObjectPool<GraniteBlockPool>()
                .ClearPool();
            ObjectPools.Instance.GetObjectPool<BoostBlockPool>()
                .ClearPool();
        }
        
        public void RestartLevel()
        {
            ClearBlockPools();
            GameInPause(false);
            _generateLevelController.ReloadLevel();
            _levelProgressController.InitLevelProgressBar();
            _lifesController.LoadLifes();
        }
    }
}