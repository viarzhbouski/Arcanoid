using Scenes.SceneMenu.Models;
using Scripts.Core.Interfaces.MVC;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace Scenes.SceneMenu.Views
{
    public class MenuView : MonoBehaviour, IView
    {
        [SerializeField]
        private Button startButton;
        
        private MenuModel _menuModel;
        
        public void Bind(IModel model, IController controller)
        {
            _menuModel = model as MenuModel;
            startButton.onClick.AddListener(StartOnClick);
            startButton.transform.DOScale(0.9f, 0.5f).SetLoops(int.MaxValue, LoopType.Yoyo);
        }

        private void StartOnClick()
        {
            startButton.transform.DOKill();
            SceneManager.LoadScene(1);
        }

        public void RenderChanges()
        {
            return;
        }
    }
}