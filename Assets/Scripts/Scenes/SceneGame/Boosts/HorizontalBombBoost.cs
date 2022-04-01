using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost;


namespace Scenes.SceneGame.Boosts
{
    public class HorizontalBombBoost : IHasBoost
    {
        private readonly BaseBlockView[,] _levelBlocks;
        private readonly List<BaseBlockView> _verticalBlocks;
        private readonly Queue<BoostBlockView> _blocksQueue;
        
        public HorizontalBombBoost(BaseBlockView[,] levelBlocks, int blockGridPositionX, int blockGridPositionY)
        {
            _levelBlocks = levelBlocks;
            _verticalBlocks = new List<BaseBlockView>();
            _blocksQueue = new Queue<BoostBlockView>();
            FillVerticalBlocks(blockGridPositionX, blockGridPositionY);
        }
        
        public void ExecuteBoost(BonusBoostView bonusBoost)
        {
            DestroyVerticalBlocks();
        }

        public void DestroyVerticalBlocks()
        {
            foreach (var block in _verticalBlocks)
            {
                if (!block.gameObject.activeSelf)
                {
                    continue;
                }
                
                switch (block.BlockType)
                {
                    case BlockTypes.Color:
                        block.BlockHit(int.MaxValue - 1);
                        break;
                    case BlockTypes.Granite:
                        block.DestroyBlock();
                        break;
                    case BlockTypes.Boost:
                        var blockBoost = (BoostBlockView)block;
                        _blocksQueue.Enqueue(blockBoost);
                        break;
                }
            }
            
            ExecuteNeighbourBoosts();
        }

        private void ExecuteNeighbourBoosts()
        {
            while (_blocksQueue.Any())
            {
                var blockBoost = _blocksQueue.Dequeue();
                blockBoost.BlockHit(int.MaxValue - 1);
            }
        }
        
        private void FillVerticalBlocks(int i, int j)
        {
            var x = i;
            
            for (var y = 0; y < _levelBlocks.GetLength(1) ; y++)
            {
                if (x == i && y == j)
                {
                    continue;
                }
                
                _verticalBlocks.Add(_levelBlocks[x, y]);
            }
        }
    }
}