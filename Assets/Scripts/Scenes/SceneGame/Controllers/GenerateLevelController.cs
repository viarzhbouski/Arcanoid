using Common.Enums;
using Newtonsoft.Json;
using UnityEngine;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.Models;
using Scripts.Helpers;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class GenerateLevelController : IController, IHasStart
    {
        private readonly GenerateLevelModel _generateLevelModel;
        private readonly GenerateLevelView _generateLevelView;
        private readonly MainConfig _mainConfig;

        public GenerateLevelController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _generateLevelModel = new GenerateLevelModel();
            _generateLevelView = view as GenerateLevelView;
            
            _generateLevelView!.Bind(_generateLevelModel, this);
            _generateLevelModel.OnChangeHandler(ControllerOnChange);
        }
        
        public void StartController()
        {
            var level = GetLevel();
            GenerateBlocksGrid(level);
        }

        private LevelMap GetLevel()
        {
            var lastLevel = GameProgressHelper.GetLastLevel();
            var pack = _mainConfig.Packs[DataRepository.Pack];
            var levelData = pack.Levels[lastLevel];
            var levelMap = JsonConvert.DeserializeObject<LevelMap>(levelData.text);
            return levelMap;
        }

        public void ControllerOnChange()
        {
            _generateLevelView.RenderChanges();
        }

        private void GenerateBlocksGrid(LevelMap level)
        {
            _generateLevelModel.CellSize = new Vector2
            {
                x = (_mainConfig.MaxWidth - _mainConfig.SpaceWidth * (level.Width + 1)) / level.Width,
                y = (_mainConfig.MaxHeight - _mainConfig.SpaceHeight * (level.Height + 1)) / level.Height
            };
            
            var blockId = 0;

            for (var i = 0; i < level.Height; i++)
            {
                for (var j = 0; j < level.Width; j++)
                {
                    var position = new Vector2
                    {
                        x = _mainConfig.Width - (_mainConfig.SpaceWidth * (j + 1) + j * _generateLevelModel.CellSize.x + _generateLevelModel.CellSize.x / 2),
                        y = _mainConfig.Height - (_mainConfig.SpaceHeight * (i + 1) + i * _generateLevelModel.CellSize.y + _generateLevelModel.CellSize.y / 2)
                    };
                    
                    _generateLevelModel.Blocks.Add(new BlockPosition
                    {
                        BlockType = (BlockTypes)level.Blocks[blockId],
                        Position = position
                    });
                    
                    blockId++;
                }
            }
            
            _generateLevelModel.OnChange?.Invoke();
        }
    }
}
