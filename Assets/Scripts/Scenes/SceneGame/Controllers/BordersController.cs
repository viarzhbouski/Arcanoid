using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;
using UnityEngine;

namespace Scenes.SceneGame.Controllers
{
    public class BordersController : IController, IHasStart
    {
        private readonly BordersModel _bordersModel;
        private readonly BordersView _bordersView;

        public BordersController(IView view)
        {
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
            var topPanelWidth = AppConfig.Instance.Gamefield.MaxViewportSize / (_bordersModel.TopPanelPosition.y / 2) * ratio;
            
            _bordersModel.TopBorderPosition = AppConfig.Instance.Gamefield.MaxViewportSize - topPanelWidth;
            _bordersModel.OnChange?.Invoke();
        }
    }
}