using Core.Interfaces;
using Core.Interfaces.MVC;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;
using ScriptableObjects;
using UnityEngine;

namespace Scenes.SceneGame.Controllers
{
    public class BordersController : IController, IHasStart
    {
        private readonly BordersModel _bordersModel;
        private readonly BordersView _bordersView;
        private readonly MainConfig _mainConfig;

        public BordersController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _bordersModel = new BordersModel();
            _bordersView = view as BordersView;
            _bordersView!.Bind(_bordersModel, this);
            _bordersModel.OnChangeHandler(ControllerOnChange);
        }

        public void ControllerOnChange()
        {
            _bordersView.RenderChanges();
        }

        public void StartController()
        {
            CalcTopBorderPosition();
        }

        private void CalcTopBorderPosition()
        {
            var ratio = (float)Screen.width / Screen.height;
            var topPanetWidth = _mainConfig.MaxViewportSize / (_bordersModel.TopPanelPosition.y / 2) * ratio;
            
            _bordersModel.TopBorderPosition = _mainConfig.MaxViewportSize - topPanetWidth;
            _bordersModel.OnChange?.Invoke();
        }
    }
}