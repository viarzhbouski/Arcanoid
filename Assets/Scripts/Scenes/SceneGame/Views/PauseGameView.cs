using Managers;
using Scenes.SceneGame.Views.Popups;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class PauseGameView : MonoBehaviour, IView
    {
        [SerializeField]
        private Button pauseButton;
        
        private PauseGameModel _pauseGameModel;
        private PauseGameController _pauseGameController;

        public void Bind(IModel model, IController controller)
        {
            _pauseGameModel = model as PauseGameModel;
            _pauseGameController = controller as PauseGameController;
            pauseButton.onClick.AddListener(PauseButtonOnClick);
        }

        private void PauseButtonOnClick()
        {
            _pauseGameController.GameInPause(true);
            PopupManager.Instance.ShowPopup<PausePopupView>();
        }

        public void RenderChanges()
        {
        }
    }
}