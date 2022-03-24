using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;
using ScriptableObjects;

namespace Scenes.SceneGame.Controllers
{
    public class LifesController : IController, IHasStart
    {
        private readonly LifesModel _lifesModel;
        private readonly LifesView _lifesView;
        private readonly MainConfig _mainConfig;

        private PauseGameController _pauseGameController;

        public LifesController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _lifesModel = new LifesModel();
            _lifesView = view as LifesView;
            _lifesView!.Bind(_lifesModel, this);
            _lifesModel.OnChangeHandler(ControllerOnChange);
            LoadLifes();
        }
        
        public void StartController()
        {
            _pauseGameController = AppControllers.Instance.GetController<PauseGameController>();
        }

        public void ControllerOnChange()
        {
            _lifesView.RenderChanges();
        }

        public void LoadLifes()
        {
            _lifesModel.IsStartGame = true;
            _lifesModel.LifesCount = _mainConfig.LifeCount > _mainConfig.MaxLifeCount ? _mainConfig.MaxLifeCount 
                                                                                      : _mainConfig.LifeCount;
            
            _lifesModel.OnChange?.Invoke();
            _lifesModel.IsStartGame = false;
        }

        public void DecreaseLife()
        {
            _lifesModel.LifesCount--;
            _lifesModel.OnChange?.Invoke();

            if (_lifesModel.LifesCount == 0)
            {
                _pauseGameController.GameInPause(true);
            }
        }

        public void RestartLevel() => _pauseGameController.RestartLevel();
    }
}