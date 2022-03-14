using Scenes.SceneMenu.Models;
using Scripts.Core.Interfaces.MVC;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        }

        private void StartOnClick()
        {
            SceneManager.LoadScene(1);
        }

        public void RenderChanges()
        {
            return;
        }
    }
}