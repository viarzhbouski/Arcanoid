using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.SceneGame.Views
{
    public class PauseGameView : MonoBehaviour, IView
    {
        [SerializeField]
        private Button pauseButton;
        
        private PauseGameModel _pauseGameModel;
        private PauseGameController _pauseGameController;
        private PausePopupView _pausePopup;

        public void Bind(IModel model, IController controller)
        {
            _pauseGameModel = model as PauseGameModel;
            _pauseGameController = controller as PauseGameController;
            pauseButton.onClick.AddListener(PauseButtonOnClick);
        }
        
        public void RenderChanges()
        {
        }

        private void PauseButtonOnClick()
        {
            AppPopups.Instance.OpenPopup<PausePopupView>();
        }
    }
}