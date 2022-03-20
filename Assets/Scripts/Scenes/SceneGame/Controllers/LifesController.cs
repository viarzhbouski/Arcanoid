using Managers;
using Scenes.SceneGame.Views.Popups;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class LifesController : IController, IHasStart
    {
        private readonly LifesModel _lifesModel;
        private readonly LifesView _lifesView;
        private readonly MainConfig _mainConfig;

        public LifesController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _lifesModel = new LifesModel();
            _lifesView = view as LifesView;
            _lifesView!.Bind(_lifesModel, this);
            _lifesModel.OnChangeHandler(ControllerOnChange);
        }

        public void ControllerOnChange()
        {
            _lifesView.RenderChanges();
        }

        public void StartController()
        {
            _lifesModel.LifesCount = _mainConfig.LifeCount;
            _lifesModel.OnChange?.Invoke();
        }

        public void DecreaseLife()
        {
            _lifesModel.LifesCount--;
            // if (_lifesModel.LifesCount == 0)
            // {
            //     PopupManager.Instance.ShowPopup<WinLevelPopupView>();
            // }
            
            _lifesModel.OnChange?.Invoke();
        }
    }
}