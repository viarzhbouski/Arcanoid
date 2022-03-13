using System.Collections.Generic;
using System.IO;
using Scenes.ScenePack.Models;
using Scenes.ScenePack.Views;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;
using Scripts.Helpers;
using Scripts.ScriptableObjects;
using UnityEngine;
using Pack = Scenes.ScenePack.Models.PacksModel.Pack;

namespace Scenes.ScenePack.Controllers
{
    public class PacksController : IController, IHasStart
    {
        private readonly PacksModel _packsModel;
        private readonly PacksView _packsView;
        private readonly MainConfig _mainConfig;

        public PacksController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _packsModel = new PacksModel();
            _packsView = view as PacksView;
            _packsView!.Bind(_packsModel, this);
            _packsModel.OnChangeHandler(ControllerOnChange);
        }
        
        public void ControllerOnChange()
        {
            _packsView.RenderChanges();
        }

        private void GetPacks()
        {
            var lastPack = GameProgressHelper.GetLastPack();
            
            for (var i = 0; i < _mainConfig.Packs.Length; i++)
            {
                var pack = new Pack
                {
                    Id = i,
                    Name = _mainConfig.Packs[i].Mame,
                    CanChoose = i <= lastPack
                };

                _packsModel.Packs.Add(pack);
            }
            
            _packsModel.OnChange?.Invoke();
        }

        public void StartController()
        {
            ObjectPools.Instance = null;
            GetPacks();
        }
    }
}