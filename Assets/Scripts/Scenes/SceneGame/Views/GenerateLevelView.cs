using System.Collections.Generic;
using Boosts;
using Common.Enums;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
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
                        SetBoost((BoostBlockView)_blocksGrid[i, j], i, j);
                    }
                }
            }
        }

        private void SetBlockTransform<T>(T blockMono, Block block, int i, int j) where T : BaseBlockView
        {
            blockMono.SetBlockConfig(block, _generateLevelModel.DestroyBlockEvent);
            blockMono.transform.position = ResizeHelper.ResizePosition(block.Position, gameCamera);
            blockMono.transform.localScale = ResizeHelper.ResizeScale(_generateLevelModel.CellSize, gameCamera, blockMono.BlockSpriteRenderer);
            _blocksGrid[i, j] = blockMono;
        }

        private void SetBoost(BoostBlockView blockMono, int i, int j)
        {
            switch (blockMono.BoostType!.Value)
            {
                case BoostTypes.Bomb:
                    blockMono.SetBoost(new BombBoost(_blocksGrid, i, j));
                    break;
                case BoostTypes.ColorChainBomb:
                    blockMono.SetBoost(new ColorChainBombBoost(_blocksGrid, i, j));
                    break;
            }
        }


        private void SetLevelUI()
        {
            levelNumber.text = _generateLevelModel.LevelNumber;
            packIcon.sprite = _generateLevelModel.PackIcon;
        }
    }
}