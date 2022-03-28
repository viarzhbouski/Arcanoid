using Common.Enums;
using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;
using UnityEngine.SceneManagement;

namespace Scenes.SceneGame.Controllers
{
    public class LevelProgressController : IController, IHasStart
    {
        private readonly LevelProgressModel _levelProgressModel;
        private readonly LevelProgressView _levelProgressView;
        
        private GenerateLevelController _generateLevelController;
        private PauseGameController _pauseGameController;

        public LevelProgressController(IView view)
        {
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
            _levelProgressModel.CurrentPack = AppConfig.Instance.Packs[DataRepository.SelectedPack];
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
                //_pauseGameController.GameInPause(true);
            }
        }

        public void SaveProgress()
        {
            var packsConfig = AppConfig.Instance.Packs;
            var currentGameProgress = GameCache.GetCurrentGameProgress();
            var selectedPack = DataRepository.SelectedPack;
            var selectedLevel = DataRepository.SelectedLevel;

            if (selectedPack < currentGameProgress.CurrentPack)
            {
                selectedLevel += 1;

                if (selectedLevel < packsConfig[selectedPack].Levels.Count)
                {
                    DataRepository.SelectedLevel = selectedLevel;
                }
                else
                {
                    DataRepository.SelectedPack = selectedPack + 1;
                    DataRepository.SelectedLevel = 0;
                }
                
                _pauseGameController.RestartLevel();
                return;
            }

            var nextLevel = currentGameProgress.CurrentLevel + 1;
            var currentPack = currentGameProgress.CurrentPack;
            var pack = packsConfig[currentPack];

            if (nextLevel < pack.Levels.Count)
            {
                currentGameProgress.CurrentLevel = nextLevel;
            }
            else
            {
                var nextPack = currentPack + 1;

                if (nextPack < packsConfig.Count)
                {
                    currentGameProgress.CurrentLevel = 0;
                    currentGameProgress.CurrentPack = nextPack;
                }
                else
                {
                    SceneManager.LoadScene((int)GameScenes.Packs);
                }
            }
            
            DataRepository.SelectedPack = currentGameProgress.CurrentPack;
            DataRepository.SelectedLevel = currentGameProgress.CurrentLevel;
            
            GameCache.SetCurrentGameProgress(currentGameProgress);
        }

        public void NextLevel()
        {
            _pauseGameController.RestartLevel();
        }
    }
}