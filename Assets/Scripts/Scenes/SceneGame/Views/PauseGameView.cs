using System.Collections;
using Core.Interfaces.MVC;
using Core.ObjectPooling;
using Core.Statics;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.ScenePools;
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
            _pauseGameController.GameInPause(true);
            _pausePopup = AppPopups.Instance.ShowPopup<PausePopupView>();
            _pausePopup.Init();
            _pausePopup.ContinueButton.onClick.AddListener(PausePopupContinueButtonOnClick);
            _pausePopup.RestartButton.onClick.AddListener(PausePopupRestartButtonOnClick);
        }

        private void PausePopupContinueButtonOnClick()
        {
            AppPopups.Instance.ClosePopup(_pausePopup);
            StartCoroutine(ContinueGame());
        } 
        
        private void PausePopupRestartButtonOnClick()
        {
            ClearBlockPools();
            AppPopups.Instance.ClosePopup(_pausePopup);
            _pauseGameController.RestartLevel();
        }
        
        private void ClearBlockPools()
        {
            ObjectPools.Instance.GetObjectPool<ColorBlockPool>()
                .ClearPool();
            ObjectPools.Instance.GetObjectPool<GraniteBlockPool>()
                .ClearPool();
            ObjectPools.Instance.GetObjectPool<BoostBlockPool>()
                .ClearPool();
        }
        
        IEnumerator ContinueGame()
        {
            yield return new WaitForSeconds(_pauseGameModel.PausePopupDelayAfterContinue);
            _pauseGameController.GameInPause(false);
        }
    }
}