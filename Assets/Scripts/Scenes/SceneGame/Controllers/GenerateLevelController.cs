using System.Linq;
using Common.Enums;
using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Models;
using Core.Statics;
using Newtonsoft.Json;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;
using ScriptableObjects;
using ScriptableObjects.BlockConfigs;
using UnityEngine;

namespace Scenes.SceneGame.Controllers
{
    public class GenerateLevelController : IController, IHasStart
    {
        private readonly GenerateLevelModel _generateLevelModel;
        private readonly GenerateLevelView _generateLevelView;
        private readonly MainConfig _mainConfig;
        private LevelProgressController _levelProgressController;

        public GenerateLevelController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _generateLevelModel = new GenerateLevelModel();
            _generateLevelView = view as GenerateLevelView;
            _generateLevelView!.Bind(_generateLevelModel, this);
            _generateLevelModel.OnChangeHandler(ControllerOnChange);
            _generateLevelModel.DestroyBlockEvent = DestroyBlock;
            LoadLevel();
        }

        public void StartController()
        {
            _levelProgressController = AppControllers.Instance.GetController<LevelProgressController>();
        }
        
        public void ReloadLevel()
        {
            ClearLoadedLevel();
            LoadLevel();
        }

        public int GetBlocksCount()
        {
            var blockCount = 0;
            
            for (var i = 0; i < _generateLevelModel.Blocks.GetLength(0); i++)
            {
                for (var j = 0; j < _generateLevelModel.Blocks.GetLength(1); j++)
                {
                    if (_generateLevelModel.Blocks[i, j].BlockType != BlockTypes.Granite)
                    {
                        blockCount++;
                    }
                }
            }

            return blockCount;
        }

        public void ControllerOnChange()
        {
            _generateLevelView.RenderChanges();
        }
        
        public void DestroyBlock()
        {
            _levelProgressController.UpdateProgressBar();
        }
        
        private void LoadLevel()
        {
            var level = GetLevel();
            GenerateBlocksGrid(level);
        }

        private void ClearLoadedLevel()
        {
            if (_generateLevelModel.Blocks != null)
            {
                _generateLevelModel.Blocks = null;
            }
        }
        
        private LevelMap GetLevel()
        {
            var selectedPack = DataRepository.SelectedPack;
            var selectedLevel = DataRepository.SelectedLevel;;
            var pack = AppConfig.Instance.Config.Packs[selectedPack];
            var levelData = pack.Levels[selectedLevel];
            var levelMap = JsonConvert.DeserializeObject<LevelMap>(levelData.text);

            _generateLevelModel.LevelNumber = $"{selectedLevel + 1}";
            _generateLevelModel.PackIcon = pack.Image;
            
            return levelMap;
        }

        private void GenerateBlocksGrid(LevelMap level)
        {
            _generateLevelModel.CellSize = new Vector2
            {
                x = (_mainConfig.MaxViewportSize - _mainConfig.SpaceWidth * (level.Columns + 1)) / level.Columns
            };
            
            var blocksGrid = new BlockInfo[level.Rows, level.Columns];
            var blockId = 0;
            var blocks = level.LevelMapData;
            var ratio = (float)Screen.width / Screen.height;
            var cellWidth = _generateLevelModel.CellSize.x / 2;
            var cellHeight = cellWidth * ratio;
            var topPanelWidth = _mainConfig.MaxViewportSize / (_generateLevelModel.TopPanelPosition.y / 2) * ratio;
            var y = _mainConfig.MaxViewportSize - cellHeight / 2 - _mainConfig.SpaceHeight - topPanelWidth;
            
            for (var i = 0; i < level.Rows; i++)
            {
                var x = cellWidth + _mainConfig.SpaceWidth;
                
                for (var j = 0; j < level.Columns; j++)
                {
                    var blockType = (BlockTypes)blocks[blockId]; ;
                    var block = AppConfig.Instance.Config.Blocks.First(e => e.BlockType == blockType);
                    var blockInfo = new BlockInfo
                    {
                        HealthPoints = block.HealthPoints,
                        Position = new Vector2(x, y),
                        BlockType = blockType
                    };

                    var blockProperty = level.LevelMapProperties.FirstOrDefault(e => e.Index == blockId);
                    
                    switch (blockType)
                    {
                        case BlockTypes.Granite:
                            var graniteBlock = (GraniteBlockConfig)block;
                            blockInfo.Color = graniteBlock.BlockColor;
                            break;
                        case BlockTypes.Color:
                            blockInfo.Color = blockProperty?.BlockColor ?? Color.white;
                            break;
                        case BlockTypes.Boost:
                            var boostBlock = (BoostBlockConfig)block;
                            var boostType = blockProperty?.BoostType ?? BoostTypes.Bomb;
                            var boostColor = boostBlock.BoostColor.First(e => e.BoostType == boostType);
                            blockInfo.Color = boostColor.Color;
                            blockInfo.BoostType = boostColor.BoostType;
                            break;
                    }
                    
                    x += _generateLevelModel.CellSize.x + _mainConfig.SpaceWidth;
                    blocksGrid[i, j] = blockInfo;
                    
                    blockId++;
                }

                y -= cellHeight + _mainConfig.SpaceHeight;
            }
            
            _generateLevelModel.Blocks = blocksGrid;
            _generateLevelModel.OnChange?.Invoke();
        }
    }
}
