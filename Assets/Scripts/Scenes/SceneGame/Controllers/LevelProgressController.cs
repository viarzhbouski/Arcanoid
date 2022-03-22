using Common.Enums;
using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Helpers;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;
using UnityEngine.SceneManagement;

namespace Scripts.Scenes.SceneGame.Controllers
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
            _generateLevelController = AppContext.Context.GetController<GenerateLevelController>();
            _pauseGameController = AppContext.Context.GetController<PauseGameController>();
            InitProgressBar();
        }

        public void ControllerOnChange()
        {
            _levelProgressView.RenderChanges();
        }
        
        public void InitProgressBar()
        {
            _levelProgressModel.IsStartGame = true;
            _levelProgressModel.BlocksAtGameField = _generateLevelController.GetBlocksCount();
            _levelProgressModel.ProgressBarStep = 1f /  _levelProgressModel.BlocksAtGameField;
            _levelProgressModel.OnChange?.Invoke();
            _levelProgressModel.IsStartGame = false;
        }

        public void UpdateProgressBar()
        {
            _levelProgressModel.BlocksAtGameField--;
            if (_levelProgressModel.BlocksAtGameField == 0)
            {
                _pauseGameController.GameInPause(true);
            }
            
            _levelProgressModel.OnChange?.Invoke();
        }

        public void LevelWin()
        {
            var currentLevel = GameProgressHelper.GetLastLevel() + 1;
            var currentPack = GameProgressHelper.GetLastPack();
            var pack = _mainConfig.Packs[currentPack];

            if (currentLevel < pack.Levels.Length)
            {
                GameProgressHelper.SetLastLevel(currentLevel);
            }
            else
            {
                currentPack += 1;
                
                if (currentPack < _mainConfig.Packs.Length)
                {
                    DataRepository.Pack = currentPack;
                    GameProgressHelper.SetLastPack(currentPack);
                    GameProgressHelper.SetLastLevel(0);
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