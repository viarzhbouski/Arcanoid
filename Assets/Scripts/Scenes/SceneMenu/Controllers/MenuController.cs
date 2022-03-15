﻿using Scenes.SceneMenu.Models;
using Scenes.SceneMenu.Views;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.ScriptableObjects;

namespace Scenes.SceneMenu.Controllers
{
    public class MenuController : IController, IHasStart
    {
        private readonly MenuModel _menuModel;
        private readonly MenuView _menuView;
        private readonly MainConfig _mainConfig;

        public MenuController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _menuModel = new MenuModel();
            _menuView = view as MenuView;
            _menuView!.Bind(_menuModel, this);
            _menuModel.OnChangeHandler(ControllerOnChange);
        }
        
        public void ControllerOnChange()
        {
            _menuView.RenderChanges();
        }
        public void StartController()
        {
        }
    }
}