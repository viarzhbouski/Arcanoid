using System.Collections.Generic;
using Boosts.Interfaces;
using Common.Enums;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using Scripts.Core.ObjectPooling;

namespace Boosts
{
    public class BombBoost : IHasBoost
    {
        private readonly List<BaseBlockView> _neighbourBlocks;
        
        public BombBoost(List<BaseBlockView> neighbourBlocks)
        {
            _neighbourBlocks = neighbourBlocks;
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
                        var blockColor = (ColorBlockView)block;
                        blockColor.BlockHit();
                        if (blockColor.CanDestroy)
                        {
                            ObjectPools.Instance.GetObjectPool<ColorBlockPool>().DestroyPoolObject(blockColor);
                        }
                        break;
                    case BlockTypes.Granite:
                        ObjectPools.Instance.GetObjectPool<GraniteBlockPool>().DestroyPoolObject((GraniteBlockView)block);
                        break;
                    case BlockTypes.Boost:
                        var blockBoost = (BoostBlockView)block;
                        blockBoost.BlockHit();
                        ObjectPools.Instance.GetObjectPool<BoostBlockPool>().DestroyPoolObject(blockBoost);
                        break;
                }
            }
        }
    }
}