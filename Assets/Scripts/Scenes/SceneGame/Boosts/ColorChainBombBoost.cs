using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost;
using UnityEngine;

namespace Scenes.SceneGame.Boosts
{
    public struct ColorBlock
    {
        public BaseBlockView Block { get; set; }
        public int Row { get; set; }
        
        public int Column { get; set; }
    }
    
    public class ColorChainBombBoost : IHasBoost
    {
        private readonly BaseBlockView[,] _levelBlocks;
        private readonly Queue<BaseBlockView> _blocksQueue;
        private List<ColorBlock> _neighbourBlocks;
        private Color? _blockColor;
        
        public ColorChainBombBoost(BaseBlockView[,] levelBlocks, int blockGridRow, int blockGridColumn, Color? blockColor = null)
        {
            _levelBlocks = levelBlocks;
            _neighbourBlocks = new List<ColorBlock>();
            _blocksQueue = new Queue<BaseBlockView>();
            _blockColor = blockColor;
            FillNeighbourBlocks(blockGridRow, blockGridColumn);
        }

        public void ExecuteBoost(BonusBoostView bonusBoost)
        {
            DestroyNeighbours();
        }
        
        public void DestroyNeighbours()
        {
            foreach (var colorBlock in _neighbourBlocks)
            {
                var block = colorBlock.Block;
                
                if (!block.gameObject.activeSelf)
                {
                    continue;
                }
                
                switch (block.BlockType)
                {
                    case BlockTypes.Color:
                        block.SetBoost(new ColorChainBombBoost(_levelBlocks, colorBlock.Row, colorBlock.Column, _blockColor));
                        _blocksQueue.Enqueue(block);
                        break;
                }
            }
            
            ExecuteNeighbourBoosts();
        }

        private void ExecuteNeighbourBoosts()
        {
            while (_blocksQueue.Any())
            {
                var colorBlock = _blocksQueue.Dequeue();
                colorBlock.BlockHit();
            }
        }

        private void FillNeighbourBlocks(int i, int j)
        {
            var top = i > 0 ? i - 1 : (int?)null;
            var bottom = i < _levelBlocks.GetLength(0) - 1 ? i + 1 : (int?)null;
            var left = j > 0 ? j - 1 : (int?)null;
            var right = j < _levelBlocks.GetLength(1) - 1 ? j + 1 : (int?)null;

            AddNeighbour(top, j);
            AddNeighbour(bottom, j);
            AddNeighbour(i, left);
            AddNeighbour(i, right);
            
            if (!_blockColor.HasValue)
            {
                var groupedBlocks = _neighbourBlocks.GroupBy(e => e.Block.BlockColor)
                                                                         .OrderByDescending(e => e.Count())
                                                                         .FirstOrDefault();
                
                _neighbourBlocks = groupedBlocks?.ToList() ?? new List<ColorBlock>();
                
            }
            
            _blockColor = _neighbourBlocks.Any() ? _neighbourBlocks.First().Block.BlockColor 
                                                 : (Color?)null;
        }

        private void AddNeighbour(int? row, int? column)
        {
            if (!row.HasValue || !column.HasValue)
            {
                return;
            }
            
            var block = _levelBlocks[row!.Value, column!.Value];
            
            if (block.BlockType == BlockTypes.Color && block.gameObject.activeSelf)
            {
                if (_blockColor.HasValue && block.BlockColor != _blockColor.Value)
                {
                    return;
                }
                
                _neighbourBlocks.Add(new ColorBlock
                {
                    Block = block,
                    Column = column!.Value,
                    Row = row!.Value
                });
            }
        }
    }
}