using Common.Enums;
using Core.Interfaces.MVC;
using Core.Models;
using Core.Statics;
using Scenes.Common;
using Scenes.ScenePack.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.ScenePacks.Views
{
    public class PackListView : MonoBehaviour, IView
    {
        [SerializeField] 
        private Button backToMainMenuButton;
        [SerializeField]
        private Transform contentTransform;
        [SerializeField]
        private PackView packPrefab;
        [SerializeField]
        private EnergyView energyView;
        
        private PackListModel _packListModel;
        
        public void Bind(IModel model, IController controller)
        {
            _packListModel = model as PackListModel;
            backToMainMenuButton.onClick.AddListener(BackToMainMenuButtonOnClick);
        }

        private void BackToMainMenuButtonOnClick()
        {
            backToMainMenuButton.enabled = false;
            AppSceneLoader.Instance.LoadScene(GameScenes.MainMenu);
        }

        public void RenderChanges()
        {
            SpawnPackButtons();
        }

        private void SpawnPackButtons()
        {
            var currentGameProgress = GameCache.GetCurrentGameProgress();
            
            foreach (var pack in _packListModel.Packs)
            {
                var packObject = Instantiate(packPrefab, contentTransform);
                packObject.PackNameUI.text = pack.Name;
                packObject.LevelProgressUI.text = $"{pack.CurrentLevel}/{pack.MaxLevels}";
                packObject.PackImageUI.sprite = pack.PackIcon;
                packObject.EnergyCostUI.text = pack.PackCost.ToString();
                
                if (pack.CanChoose)
                {
                    packObject.PackButtonUI.onClick.AddListener(delegate { PackOnClick(pack.Id,  pack.PackCost, currentGameProgress); });
                }
                else
                {
                    packObject.PackButtonUI.enabled = false;
                }
            }
        }

        private void PackOnClick(int packId, int packCost, GameProgress currentGameProgress)
        {
            DataRepository.SelectedPack = packId;
            DataRepository.SelectedLevel = packId == currentGameProgress.CurrentPack ? currentGameProgress.CurrentLevel : 0;
            
            if (packId == AppConfig.Instance.Packs.Count - 1 &&  currentGameProgress.CurrentLevel == AppConfig.Instance.Packs[packId].Levels.Count - 1)
            {
                DataRepository.SelectedLevel = 0;
            }

            var currentEnergy = energyView.CurrentEnergy;
            if (currentEnergy >= packCost)
            {
                energyView.UseEnergy(packCost);
                AppSceneLoader.Instance.LoadScene(GameScenes.Game);
            }
        }
    }
}