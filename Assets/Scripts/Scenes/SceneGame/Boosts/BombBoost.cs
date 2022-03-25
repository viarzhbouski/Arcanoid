using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Views.PoolableViews.Blocks;

namespace Scenes.SceneGame.Boosts
{
    public class BombBoost : IHasBoost
    {
        private readonly BaseBlockView[,] _levelBlocks;
        private readonly List<BaseBlockView> _neighbourBlocks;
        private readonly Queue<BoostBlockView> _blocksQueue;
        
        public BombBoost(BaseBlockView[,] levelBlocks, int blockGridPositionX, int blockGridPositionY)
        {
            _levelBlocks = levelBlocks;
            _neighbourBlocks = new List<BaseBlockView>();
            _blocksQueue = new Queue<BoostBlockView>();
            FillNeighbourBlocks(blockGridPositionX, blockGridPositionY);
        }
        
        public void ExecuteBoost()
        {
            DestroyNeighbours();
        }

        public void DestroyNeighbours()
        {
            foreach (var block in _neighbourBlocks)
            {
                if (!block.gameObject.activeSelf)
                {
                    continue;
                }
                
                switch (block.BlockType)
                {
                    case BlockTypes.Color:
                        block.BlockHit(555);
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
                blockBoost.BlockHit();
            }
        }
        
        private void FillNeighbourBlocks(int i, int j)
        {
            var rowMinimum = i - 1 < 0 ? i : i - 1;
            var rowMaximum = i + 1 > _levelBlocks.GetLength(0) - 1 ? i : i + 1;
            var columnMinimum = j - 1 < 0 ? j : j - 1;
            var columnMaximum = j + 1 > _levelBlocks.GetLength(1) - 1 ? j : j + 1;

            for (var x = rowMinimum; x <= rowMaximum; x++)
            {
                for (var y = columnMinimum; y <= columnMaximum; y++)
                {
                    if (x != i || y != j)
                    {
                        _neighbourBlocks.Add(_levelBlocks[x, y]);
                    }
                }
            }
        }
    }
}