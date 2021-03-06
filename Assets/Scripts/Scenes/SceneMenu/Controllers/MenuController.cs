using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneMenu.Models;
using Scenes.SceneMenu.Views;
using ScriptableObjects;
using UnityEngine;

namespace Scenes.SceneMenu.Controllers
{
    public class MenuController : IController
    {
        private readonly MenuModel _menuModel;
        private readonly MenuView _menuView;

        public MenuController(IView view)
        {
            _menuModel = new MenuModel();
            _menuView = view as MenuView;
            _menuView!.Bind(_menuModel, this);
            _menuModel.OnChangeHandler(ControllerOnChange);
        }
        
        public void ControllerOnChange()
        {
            _menuView.RenderChanges();
        }
    }
}