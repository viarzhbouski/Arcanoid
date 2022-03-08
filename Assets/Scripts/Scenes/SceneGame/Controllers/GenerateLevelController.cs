using System.IO;
using Common.Enums;
using Newtonsoft.Json;
using UnityEngine;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.Models;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class GenerateLevelController : IController, IHasStart
    {
        private readonly GenerateLevelModel _generateLevelModel;
        private readonly GenerateLevelView _generateLevelView;
        
        private const float Width = 1.0f;
        private const float Height = 0.9f;
        private const float MaxWidth = 1.0f;
        private const float MaxHeight = 0.5f;
        private const float SpaceWidth  = 0.01f;
        private const float SpaceHeight = 0.01f;

        public GenerateLevelController(IView view)
        {
            _generateLevelModel = new GenerateLevelModel();
            _generateLevelView = view as GenerateLevelView;
            
            _generateLevelView!.Bind(_generateLevelModel);
            _generateLevelModel.OnChangeHandler(ControllerOnChange);
        }
        
        public void StartController()
        {
            var level = GetLevelMap();
            GenerateBlocksGrid(level);
        }

        public void ControllerOnChange()
        {
            _generateLevelView.RenderChanges();
        }

        private void GenerateBlocksGrid(LevelMap level)
        {
            _generateLevelModel.CellSize = new Vector2
            {
                x = (MaxWidth - SpaceWidth * (level.Width + 1)) / level.Width,
                y = (MaxHeight - SpaceHeight * (level.Height + 1)) / level.Height
            };
            
            var blockId = 0;

            for (var i = 0; i < level.Height; i++)
            {
                for (var j = 0; j < level.Width; j++)
                {
                    var position = new Vector2
                    {
                        x = Width - (SpaceWidth * (j + 1) + j * _generateLevelModel.CellSize.x + _generateLevelModel.CellSize.x / 2),
                        y = Height - (SpaceHeight * (i + 1) + i * _generateLevelModel.CellSize.y + _generateLevelModel.CellSize.y / 2)
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
        
        /// <summary>
        /// пока как тест
        /// </summary>
        /// <returns></returns>
        private LevelMap GetLevelMap()
        {
            var json = File.ReadAllText("./Assets/Levels/level_0.json");
            var level = JsonConvert.DeserializeObject<LevelMap>(json);
            return level;
        }
    }
}
