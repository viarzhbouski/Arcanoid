using System.Linq;
using Common.Enums;
using Core.Interfaces.MVC;
using Core.Models;
using Core.Statics;
using Scenes.SceneMenu.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using ScriptableObjects;
using TMPro;

namespace Scenes.SceneMenu.Views
{
    public class MenuView : MonoBehaviour, IView
    {
        [SerializeField]
        private Button startButton;
        
        [SerializeField]
        private Button localizationButton;

        [SerializeField]
        private Image localizationButtonImage;
        
        [SerializeField] 
        private TMP_Text logoText;
        
        private MenuModel _menuModel;
        private LocalizationConfig _currentLocalization;
        
        public void Bind(IModel model, IController controller)
        {
            logoText.text = Localization.GetFieldText("Title");
            _menuModel = model as MenuModel;
            startButton.onClick.AddListener(StartOnClick);
            localizationButton.onClick.AddListener(LocalizationOnClick);
            startButton.transform.DOKill();
            startButton.transform.DOScale(0.9f, 0.5f).SetLoops(int.MaxValue, LoopType.Yoyo);
            InitLocalizationButton();
        }

        private void StartOnClick()
        {
            startButton.enabled = false;
            startButton.transform.DOKill();
            
            var currentGameProgress = GameCache.GetCurrentGameProgress();
            if (currentGameProgress.CurrentPack == 0 && currentGameProgress.CurrentLevel == 0)
            {
                DataRepository.SelectedLevel = currentGameProgress.CurrentLevel;
                DataRepository.SelectedPack = currentGameProgress.CurrentPack;
                AppSceneLoader.Instance.LoadScene(GameScenes.Game);
            }
            else
            {
                AppSceneLoader.Instance.LoadScene(GameScenes.Packs);
            }
        }

        public void RenderChanges()
        {
        }

        private void InitLocalizationButton()
        {
            var currentLocalization = GameCache.GetCurrentLocalization();
            _currentLocalization = AppConfig.Instance.Localizations.First(e => e.LocaleLanguage == currentLocalization);
            localizationButtonImage.sprite = _currentLocalization.Flag;
        }
        
        private void LocalizationOnClick()
        {
            var newLocalizationLang = Localization.ToogleLocalization(_currentLocalization.LocaleLanguage);
            _currentLocalization = AppConfig.Instance.Localizations.First(e => e.LocaleLanguage == newLocalizationLang);

            localizationButtonImage.sprite = _currentLocalization.Flag;
            logoText.text = Localization.GetFieldText("Title");
        }
    }
}