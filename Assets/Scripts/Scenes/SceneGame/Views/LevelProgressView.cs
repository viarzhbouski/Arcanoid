using DG.Tweening;
using Managers;
using Scenes.SceneGame.Views.Popups;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;
using Scripts.Scenes.SceneGame.Controllers.Models;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class LevelProgressView : MonoBehaviour, IView
    {
        [SerializeField]
        private RectTransform progressBar;
        
        private LevelProgressModel _levelProgressModel;
        private LevelProgressController _levelProgressController;
        private WinLevelPopupView _winLevelPopupView;

        public void Bind(IModel model, IController controller)
        {
            _levelProgressModel = model as LevelProgressModel;
            _levelProgressController = controller as LevelProgressController;
        }
        
        public void RenderChanges()
        {
            if (_levelProgressModel.BlocksAtGameField == 0)
            {
                _winLevelPopupView = PopupManager.Instance.ShowPopup<WinLevelPopupView>();
                _winLevelPopupView.NextLevelButton.onClick.AddListener(NextLevelButtonOnClick);
            }
            else
            {
                if (!_levelProgressModel.IsStartGame)
                {
                    progressBar.DOScaleX(progressBar.localScale.x + _levelProgressModel.ProgressBarStep, 0.2f);
                }
                else
                {
                    var progressBarScale = progressBar.localScale;
                    progressBarScale.x = 0f;
                    progressBar.localScale = progressBarScale;
                }
            }
        }

        private void NextLevelButtonOnClick()
        {
            ClearBlockPools();
            _levelProgressController.LevelWin();
            PopupManager.Instance.ClosePopup(_winLevelPopupView);
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