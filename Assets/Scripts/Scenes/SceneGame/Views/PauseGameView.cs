using System.Collections;
using Managers;
using Scenes.SceneGame.Views.Popups;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;
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
            _pauseGameController.GameInPause(true);
            _pausePopup = PopupManager.Instance.ShowPopup<PausePopupView>();
            _pausePopup.ContinueButton.onClick.AddListener(PausePopupContinueButtonOnClick);
            _pausePopup.RestartButton.onClick.AddListener(PausePopupRestartButtonOnClick);
        }

        private void PausePopupContinueButtonOnClick()
        {
            PopupManager.Instance.ClosePopup(_pausePopup);
            StartCoroutine(ContinueGame());
        } 
        
        private void PausePopupRestartButtonOnClick()
        {
            var objectPool = ObjectPools.Instance.GetObjectPool<BlockPoolManager>();
            objectPool.ClearPool();
            PopupManager.Instance.ClosePopup(_pausePopup);
            _pauseGameController.RestartLevel();
            _pauseGameController.GameInPause(false);
        }

        IEnumerator ContinueGame()
        {
            yield return new WaitForSeconds(_pauseGameModel.PausePopupDelayAfterContinue);
            _pauseGameController.GameInPause(false);
        }
    }
}