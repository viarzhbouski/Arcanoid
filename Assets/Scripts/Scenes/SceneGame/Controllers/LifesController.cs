using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;

namespace Scenes.SceneGame.Controllers
{
    public class LifesController : IController, IHasStart
    {
        private readonly LifesModel _lifesModel;
        private readonly LifesView _lifesView;

        private PauseGameController _pauseGameController;

        public LifesController(IView view)
        {
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
            var ballAndPlatform = AppConfig.Instance.BallAndPlatform;
            _lifesModel.IsStartGame = true;
            _lifesModel.LifesCount = ballAndPlatform.LifeCount > ballAndPlatform.MaxLifeCount ? ballAndPlatform.MaxLifeCount 
                                                                                                : ballAndPlatform.LifeCount;
            
            _lifesModel.OnChange?.Invoke();
            _lifesModel.IsStartGame = false;
        }

        public void DecreaseLife()
        {
            _lifesModel.LifesCount--;
            _lifesModel.OnChange?.Invoke();
        }

        public void EncreaseLife()
        {
            _lifesModel.LifesCount++;
            _lifesModel.OnChange?.Invoke();
        }

        public void RestartLevel() => _pauseGameController.RestartLevel();
    }
}