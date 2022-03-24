using System.Collections;
using Core.Interfaces.MVC;
using Core.ObjectPooling;
using Core.Statics;
using DG.Tweening;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views.Popups;
using UnityEngine;

namespace Scenes.SceneGame.Views
{
    public class LevelProgressView : MonoBehaviour, IView
    {
        [SerializeField]
        private RectTransform progressBar;
        
        private LevelProgressModel _levelProgressModel;
        private LevelProgressController _levelProgressController;
        private WinLevelPopupView _winLevelPopupView;
        private const float WinPopupDelay = 0.75f;
        
        public void Bind(IModel model, IController controller)
        {
            _levelProgressModel = model as LevelProgressModel;
            _levelProgressController = controller as LevelProgressController;
            _levelProgressModel!.ProgressBarXPosition = progressBar.localScale.x;
        }
        
        public void RenderChanges()
        {
            
            if (_levelProgressModel.BlocksAtGameField == 0)
            {
                StartCoroutine(OpenWinPopup());
            }
            
            if (!_levelProgressModel.IsStartGame)
            {
                _levelProgressModel!.ProgressBarXPosition += _levelProgressModel.ProgressBarStep;
                progressBar.DOKill();
                progressBar.DOScaleX(_levelProgressModel!.ProgressBarXPosition, 0.1f);
            }
            else
            {
                var progressBarScale = progressBar.localScale;
                progressBarScale.x = 0f;
                progressBar.localScale = progressBarScale;
            }
        }

        IEnumerator OpenWinPopup()
        {
            yield return new WaitForSeconds(WinPopupDelay);
            _winLevelPopupView = AppPopups.Instance.ShowPopup<WinLevelPopupView>();
            _winLevelPopupView.NextLevelButton.onClick.AddListener(NextLevelButtonOnClick);
        }

        private void NextLevelButtonOnClick()
        {
            ClearBlockPools();
            _levelProgressController.LevelWin();
            AppPopups.Instance.ClosePopup(_winLevelPopupView);
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
    }
}