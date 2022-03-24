using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.ObjectPooling;
using Core.Statics;
using Scenes.ScenePack.Models;
using Scenes.ScenePacks.Views;
using ScriptableObjects;
using Pack = Scenes.ScenePack.Models.PackListModel.Pack;

namespace Scenes.ScenePacks.Controllers
{
    public class PackListController : IController, IHasStart
    {
        private readonly PackListModel _packListModel;
        private readonly PackListView _packListView;
        private readonly MainConfig _mainConfig;

        public PackListController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _packListModel = new PackListModel();
            _packListView = view as PackListView;
            _packListView!.Bind(_packListModel, this);
            _packListModel.OnChangeHandler(ControllerOnChange);
        }
        
        public void ControllerOnChange()
        {
            _packListView.RenderChanges();
        }

        private void GetPacks()
        {
            var lastLevel = GameProgress.GetLastLevel();
            var lastPack = GameProgress.GetLastPack();
            
            for (var i = 0; i < _mainConfig.Packs.Length; i++)
            {
                var canChoose = i <= lastPack;
                var pack = new Pack
                {
                    Id = i,
                    Name = canChoose ? _mainConfig.Packs[i].Name : "???",
                    CurrentLevel = canChoose ? lastLevel : 0,
                    MaxLevels = _mainConfig.Packs[i].Levels.Length,
                    PackIcon = canChoose ? _mainConfig.Packs[i].Image : null,
                    CanChoose = canChoose
                };

                _packListModel.Packs.Add(pack);
            }
            
            _packListModel.OnChange?.Invoke();
        }

        public void StartController()
        {
            ObjectPools.Instance = null;
            GetPacks();
        }
    }
}