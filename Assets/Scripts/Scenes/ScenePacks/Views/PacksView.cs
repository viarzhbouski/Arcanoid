using MonoModels;
using Scenes.ScenePack.Models;
using Scripts.Core.Interfaces.MVC;
using Scripts.Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes.ScenePack.Views
{
    public class PacksView : MonoBehaviour, IView
    {
        [SerializeField] 
        private Button backToMainMenuButton;
        [SerializeField]
        private Transform contentTransform;
        
        [SerializeField]
        private PackMono packPrefab;
        
        private PacksModel _packsModel;
        
        public void Bind(IModel model, IController controller)
        {
            _packsModel = model as PacksModel;
            backToMainMenuButton.onClick.AddListener(BackToMainMenuButtonOnClick);
        }

        private void BackToMainMenuButtonOnClick()
        {
            SceneManager.LoadScene(0);
        }

        public void RenderChanges()
        {
            SpawnPackButtons();
        }

        private void SpawnPackButtons()
        {
            foreach (var pack in _packsModel.Packs)
            {
                var packObject = Instantiate(packPrefab, contentTransform);
                packObject.PackNameUI.text = pack.Name;
                
                if (pack.CanChoose)
                {
                    packObject.PackButtonUI.onClick.AddListener(delegate { PackOnClick(pack.Id); });
                }
                else
                {
                    packObject.PackButtonUI.enabled = false;
                }
            }
        }

        private void PackOnClick(int packId)
        {
            DataRepository.Pack = packId;
            SceneManager.LoadScene(2);
        }
    }
}