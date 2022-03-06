using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class BallController : IController, IHasStart
    {
        private readonly BallModel _ballModel;
        private readonly BallView _ballView;

        public BallController(IView view)
        {
            _ballModel = new BallModel();
            _ballView = view as BallView;
            _ballView!.Bind(_ballModel);
            _ballModel.OnChangeHandler(ControllerOnChange);
        }

        public void ControllerOnChange()
        {
            _ballView.RenderChanges();
        }

        public void StartController()
        {
            _ballModel.Speed = 500f;
            _ballModel.OnChange?.Invoke();
        }
    }
}