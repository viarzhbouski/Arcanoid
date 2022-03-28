using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.ObjectPooling;
using Core.Statics;
using Scenes.ScenePack.Models;
using Scenes.ScenePacks.Views;
using Pack = Scenes.ScenePack.Models.PackListModel.Pack;

namespace Scenes.ScenePacks.Controllers
{
    public class PackListController : IController, IHasStart
    {
        private readonly PackListModel _packListModel;
        private readonly PackListView _packListView;

        public PackListController(IView view)
        {
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
            var currentGameProgress = GameCache.GetCurrentGameProgress();
            var packsConfig = AppConfig.Instance.Packs;
            
            for (var i = 0; i < packsConfig.Count; i++)
            {
                var packConfig = packsConfig[i];
                var currentLevel = i == currentGameProgress.CurrentPack ? currentGameProgress.CurrentLevel
                                                                            : packConfig.Levels.Count;
                
                var canChoose = i <= currentGameProgress.CurrentPack;
                var pack = new Pack
                {
                    Id = i,
                    Name = canChoose ? Localization.GetFieldText($"Pack{i + 1}") : "???",
                    CurrentLevel = canChoose ? currentLevel : 0,
                    MaxLevels = packConfig.Levels.Count,
                    PackIcon = canChoose ? packConfig.Image : null,
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