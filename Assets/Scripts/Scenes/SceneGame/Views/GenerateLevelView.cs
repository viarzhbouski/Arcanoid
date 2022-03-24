using System.Collections.Generic;
using Boosts;
using Common.Enums;
using Scenes.SceneGame.Views.Blocks;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Helpers;
using Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class GenerateLevelView : MonoBehaviour, IView
    {
        [SerializeField]
        private Camera gameCamera;
        
        [SerializeField]
        private Transform mapPivot;
        
        [SerializeField]
        private RectTransform topPanel;

        [SerializeField]
        private TMP_Text levelNumber;
        
        [SerializeField]
        private Image packIcon;

        private BaseBlockView[,] _blocksGrid;

        private GenerateLevelModel _generateLevelModel;

        public void Bind(IModel model, IController controller)
        {
            _generateLevelModel = model as GenerateLevelModel;
            _generateLevelModel!.StartPosition = mapPivot.position;
            _generateLevelModel.TopPanelPosition = topPanel.transform.position;
        }
        
        public void RenderChanges()
        {
            RenderLevelMap();
        }

        private void RenderLevelMap()
        {
            var height = _generateLevelModel.Blocks.GetLength(0);
            var width = _generateLevelModel.Blocks.GetLength(1);
            _blocksGrid = new BaseBlockView[height, width];
            
            SetLevelUI();
            
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var block = _generateLevelModel.Blocks[i, j];
                    if (block.BlockType == BlockTypes.Empty)
                    {
                        continue;
                    }

                    switch (block.BlockType)
                    {
                        case BlockTypes.Empty:
                            continue;
                        case BlockTypes.Color:
                            SetBlockTransform(ObjectPools.Instance.GetObjectPool<ColorBlockPool>().GetObject(), block, i, j);
                            break;
                        case BlockTypes.Granite:
                            SetBlockTransform(ObjectPools.Instance.GetObjectPool<GraniteBlockPool>().GetObject(), block, i, j);
                            break;
                        case BlockTypes.Boost:
                            SetBlockTransform(ObjectPools.Instance.GetObjectPool<BoostBlockPool>().GetObject(), block, i, j);
                            break;
                    }
                }
            }

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (_blocksGrid[i, j].BlockType == BlockTypes.Boost)
                    {
                        SetBoost(_blocksGrid[i, j], i, j);
                    }
                }
            }
        }

        private void SetBlockTransform<T>(T blockMono, Block block, int i, int j) where T : BaseBlockView
        {
            blockMono.SetBlockConfig(block);
            blockMono.transform.position = ResizeHelper.ResizePosition(block.Position, gameCamera);
            blockMono.transform.localScale = ResizeHelper.ResizeScale(_generateLevelModel.CellSize, gameCamera, blockMono.BlockSpriteRenderer);
            _blocksGrid[i, j] = blockMono;
        }

        private void SetBoost(BaseBlockView blockMono, int i, int j)
        {
            var neighbourBlocks = GetNeighbourBlocks(i, j);
            
            switch (blockMono.BoostType!.Value)
            {
                case BoostTypes.Bomb:
                    blockMono.SetBoost(new BombBoost(neighbourBlocks));
                    break;
                case BoostTypes.ColorChainBomb:
                    blockMono.SetBoost(new ColorChainBombBoost(neighbourBlocks));
                    break;
            }
        }

        private List<BaseBlockView> GetNeighbourBlocks(int i, int j)
        {
            var neighbourBlocks = new List<BaseBlockView>();
            var rowMinimum = i - 1 < 0 ? i : i - 1;
            var rowMaximum = i + 1 > _blocksGrid.GetLength(0) - 1 ? i : i + 1;
            var columnMinimum = j - 1 < 0 ? j : j - 1;
            var columnMaximum = j + 1 > _blocksGrid.GetLength(1) - 1 ? j : j + 1;

            for (var x = rowMinimum; x <= rowMaximum; x++)
            {
                for (var y = columnMinimum; y <= columnMaximum; y++)
                {
                    if (x != i || y != j)
                    {
                        neighbourBlocks.Add(_blocksGrid[x, y]);
                    }
                }
            }

            
            return neighbourBlocks;
        }

        private void SetLevelUI()
        {
            levelNumber.text = _generateLevelModel.LevelNumber;
            packIcon.sprite = _generateLevelModel.PackIcon;
        }
    }
}