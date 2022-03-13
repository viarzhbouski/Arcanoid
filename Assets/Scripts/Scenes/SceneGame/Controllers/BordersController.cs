using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;

namespace Scripts.Scenes.SceneGame.Controllers
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
            _bordersModel.OnChange?.Invoke();
        }
    }
}