using Scenes.ScenePack.Models;
using Scenes.ScenePack.Views;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;
using Scripts.Helpers;
using Scripts.ScriptableObjects;
using Pack = Scenes.ScenePack.Models.PackListModel.Pack;

namespace Scenes.ScenePack.Controllers
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
            var lastPack = GameProgressHelper.GetLastPack();
            
            for (var i = 0; i < _mainConfig.Packs.Length; i++)
            {
                var pack = new Pack
                {
                    Id = i,
                    Name = _mainConfig.Packs[i].Mame,
                    CanChoose = i <= lastPack
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