using Managers;
using Scenes.SceneGame.Views.Popups;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class LifesController : IController
    {
        private readonly LifesModel _lifesModel;
        private readonly LifesView _lifesView;
        private readonly MainConfig _mainConfig;

        public LifesController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _lifesModel = new LifesModel();
            _lifesView = view as LifesView;
            _lifesView!.Bind(_lifesModel, this);
            _lifesModel.OnChangeHandler(ControllerOnChange);
            LoadLifes();
        }

        public void ControllerOnChange()
        {
            _lifesView.RenderChanges();
        }

        public void LoadLifes()
        {
            _lifesModel.IsStartGame = true;
            _lifesModel.LifesCount = _mainConfig.LifeCount > _mainConfig.MaxLifeCount ? _mainConfig.MaxLifeCount 
                                                                                      : _mainConfig.LifeCount;
            
            _lifesModel.OnChange?.Invoke();
            _lifesModel.IsStartGame = false;
        }

        public void DecreaseLife()
        {
            _lifesModel.LifesCount--;
            _lifesModel.OnChange?.Invoke();
        }
    }
}