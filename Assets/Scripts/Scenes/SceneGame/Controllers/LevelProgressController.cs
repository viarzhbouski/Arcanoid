using Managers;
using Scenes.SceneGame.Views.Popups;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class LevelProgressController : IController
    {
        private readonly LevelProgressModel _levelProgressModel;
        private readonly LevelProgressView _levelProgressView;
        private readonly GenerateLevelController _generateLevelController;
        private readonly MainConfig _mainConfig;

        public LevelProgressController(IView view, GenerateLevelController generateLevelController, MainConfig mainConfig)
        {
            _generateLevelController = generateLevelController;
            _mainConfig = mainConfig;
            _levelProgressModel = new LevelProgressModel();
            _levelProgressView = view as LevelProgressView;
            _levelProgressView!.Bind(_levelProgressModel, this);
            _levelProgressModel.OnChangeHandler(ControllerOnChange);
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
            _levelProgressModel.OnChange?.Invoke();
            _levelProgressModel.BlocksAtGameField--;
        } 
    }
}