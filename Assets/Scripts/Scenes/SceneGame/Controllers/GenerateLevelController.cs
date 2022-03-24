using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Models;
using Core.ObjectPooling;
using Core.Statics;
using Newtonsoft.Json;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;
using ScriptableObjects;
using UnityEngine;

namespace Scenes.SceneGame.Controllers
{
    public class GenerateLevelController : IController, IHasStart
    {
        private readonly GenerateLevelModel _generateLevelModel;
        private readonly GenerateLevelView _generateLevelView;
        private readonly MainConfig _mainConfig;
        private readonly Dictionary<BlockTypes, Block> _blocks;
        private LevelProgressController _levelProgressController;

        public GenerateLevelController(IView view, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _generateLevelModel = new GenerateLevelModel();
            _generateLevelView = view as GenerateLevelView;
            _blocks = new Dictionary<BlockTypes, Block>();
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
            FillDict();
            GenerateBlocksGrid(level);
        }

        private void ClearLoadedLevel()
        {
            if (_generateLevelModel.Blocks != null)
            {
                _generateLevelModel.Blocks = null;
            }

            if (_blocks.Any())
            {
                _blocks.Clear();
            }
        }
        
        private LevelMap GetLevel()
        {
            var lastLevel = GameProgress.GetLastLevel();
            var pack = _mainConfig.Packs[DataRepository.Pack];
            var levelData = pack.Levels[lastLevel];
            var levelMap = JsonConvert.DeserializeObject<LevelMap>(levelData.text);

            _generateLevelModel.LevelNumber = $"{lastLevel + 1}";
            _generateLevelModel.PackIcon = pack.Image;
            
            return levelMap;
        }

        private void GenerateBlocksGrid(LevelMap level)
        {
            _generateLevelModel.CellSize = new Vector2
            {
                x = (_mainConfig.MaxViewportSize - _mainConfig.SpaceWidth * (level.Width + 1)) / level.Width
            };
            
            var blocksGrid = new Block[level.Height, level.Width];
            var blockId = 0;
            var blocks = level.Layers.First().Data;
            var ratio = (float)Screen.width / Screen.height;
            var cellWidth = _generateLevelModel.CellSize.x / 2;
            var cellHeight = cellWidth * ratio;
            var topPanelWidth = _mainConfig.MaxViewportSize / (_generateLevelModel.TopPanelPosition.y / 2) * ratio;
            var y = _mainConfig.MaxViewportSize - cellHeight / 2 - _mainConfig.SpaceHeight - topPanelWidth;
            
            for (var i = 0; i < level.Height; i++)
            {
                var x = cellWidth + _mainConfig.SpaceWidth;
                
                for (var j = 0; j < level.Width; j++)
                {
                    var position = new Vector2(x, y);
                    BoostTypes? boostType = null;
                    
                    if ((int)BlockTypes.Boost < blocks[blockId])
                    {
                        boostType = (BoostTypes)blocks[blockId];
                        blocks[blockId] = (int)BlockTypes.Boost;
                    }
                    
                    var block = _blocks[(BlockTypes)blocks[blockId]];
                    block.Position = position;
                    x += _generateLevelModel.CellSize.x + _mainConfig.SpaceWidth;
                    blockId++;

                    if (boostType.HasValue)
                    {
                        block.BoostType = boostType!.Value;
                    }

                    blocksGrid[i, j] = block;
                }

                y -= cellHeight + _mainConfig.SpaceHeight;
            }

            _generateLevelModel.Blocks = blocksGrid;
            _generateLevelModel.OnChange?.Invoke();
        }

        private void FillDict()
        {
            foreach (var block in _mainConfig.Blocks)
            {
                _blocks.Add(block.BlockType, block);
            }
        }
    }
}
