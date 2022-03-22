using MonoModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.SceneGame.Views.Popups
{
    public class WinLevelPopupView : BasePopupView
    {
        [SerializeField]
        private Image packImage;
        
        [SerializeField]
        private Button nextLevelButton;
        
        [SerializeField]
        private TMP_Text packName;

        [SerializeField]
        private RectTransform progressBar;
        
        public Image PackImage => packImage;
        
        public Button NextLevelButton => nextLevelButton;

        public TMP_Text PackName => packName;

        public RectTransform ProgressBar => progressBar;
    }
}