﻿using Common.Enums;
using Core.Interfaces.MVC;
using Core.Models;
using Core.Statics;
using Scenes.ScenePack.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        
        private PackListModel _packListModel;
        
        public void Bind(IModel model, IController controller)
        {
            _packListModel = model as PackListModel;
            backToMainMenuButton.onClick.AddListener(BackToMainMenuButtonOnClick);
        }

        private void BackToMainMenuButtonOnClick()
        {
            SceneManager.LoadScene((int)GameScenes.MainMenu);
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
                
                if (pack.CanChoose)
                {
                    packObject.PackButtonUI.onClick.AddListener(delegate { PackOnClick(pack.Id, currentGameProgress); });
                }
                else
                {
                    packObject.PackButtonUI.enabled = false;
                }
            }
        }

        private void PackOnClick(int packId, GameProgress currentGameProgress)
        {
            DataRepository.SelectedPack = packId;
            DataRepository.SelectedLevel = packId == currentGameProgress.CurrentPack ? currentGameProgress.CurrentLevel : 0;
            SceneManager.LoadScene((int)GameScenes.Game);
        }
    }
}