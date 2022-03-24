using Common.Enums;
using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;
using ScriptableObjects;
using UnityEngine.SceneManagement;

namespace Scenes.SceneGame.Controllers
{
    public class LevelProgressController : IController, IHasStart
    {
        private readonly LevelProgressModel _levelProgressModel;
        private readonly LevelProgressView _levelProgressView;
        private readonly MainConfig _mainConfig;
        
        private GenerateLevelController _generateLevelController;
        private PauseGameController _pauseGameController;

        public LevelProgressController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _levelProgressModel = new LevelProgressModel();
            _levelProgressView = view as LevelProgressView;
            _levelProgressView!.Bind(_levelProgressModel, this);
            _levelProgressModel.OnChangeHandler(ControllerOnChange);
        }
        
        public void StartController()
        {
            _generateLevelController = AppControllers.Instance.GetController<GenerateLevelController>();
            _pauseGameController = AppControllers.Instance.GetController<PauseGameController>();
            InitLevelProgressBar();
        }

        public void ControllerOnChange()
        {
            _levelProgressView.RenderChanges();
        }
        
        public void InitLevelProgressBar()
        {
            _levelProgressModel.CurrentPack = _mainConfig.Packs[GameProgress.GetLastPack()];
            _levelProgressModel.IsStartGame = true;
            _levelProgressModel.BlocksAtGameField = _generateLevelController.GetBlocksCount();
            _levelProgressModel.LevelProgressBarXPosition = 0f;
            _levelProgressModel.LevelProgressBarStep = 1f / _levelProgressModel.BlocksAtGameField;
            _levelProgressModel.OnChange?.Invoke();
            _levelProgressModel.IsStartGame = false;
        }

        public void UpdateProgressBar()
        {
            _levelProgressModel.BlocksAtGameField--;
            _levelProgressModel.OnChange?.Invoke();
            
            if (_levelProgressModel.BlocksAtGameField == 0)
            {
                _pauseGameController.GameInPause(true);
            }
        }

        public void LevelWin()
        {
            var currentLevel = GameProgress.GetLastLevel() + 1;
            var currentPack = GameProgress.GetLastPack();
            var pack = _mainConfig.Packs[currentPack];

            if (currentLevel < pack.Levels.Length)
            {
                GameProgress.SetLastLevel(currentLevel);
            }
            else
            {
                currentPack += 1;
                
                if (currentPack < _mainConfig.Packs.Length)
                {
                    DataRepository.Pack = currentPack;
                    GameProgress.SetLastPack(currentPack);
                    GameProgress.SetLastLevel(0);
                }
                else
                {
                    SceneManager.LoadScene((int)GameScenes.Packs);
                }
            }
            
            _pauseGameController.RestartLevel();
        }
    }
}