using Core.Interfaces.MVC;
using Core.Statics;
using DG.Tweening;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.Models;
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
        
        public void Bind(IModel model, IController controller)
        {
            _levelProgressModel = model as LevelProgressModel;
            _levelProgressController = controller as LevelProgressController;
            _levelProgressModel!.LevelProgressBarXPosition = progressBar.localScale.x;
        }
        
        public void RenderChanges()
        {
            if (_levelProgressModel.BlocksAtGameField == 0)
            {
                AppPopups.Instance.OpenPopup<WinLevelPopupView>();
            }
            
            if (!_levelProgressModel.IsStartGame)
            {
                _levelProgressModel!.LevelProgressBarXPosition += _levelProgressModel.LevelProgressBarStep;
                progressBar.DOKill();
                progressBar.DOScaleX(_levelProgressModel!.LevelProgressBarXPosition, 0.1f);
            }
            else
            {
                var progressBarScale = progressBar.localScale;
                progressBarScale.x = 0f;
                progressBar.localScale = progressBarScale;
            }
        }
    }
}