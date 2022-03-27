using Common.Enums;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using UnityEngine;

namespace ScriptableObjects.BlockConfigs
{
    public class BaseBlockConfig : ScriptableObject
    {
        [SerializeField]
        private int healthPoints;
        [SerializeField]
        private BlockTypes blockType;
        [SerializeField]
        private BaseBlockView blockPrefab;
        
        public Vector2 Position { get; set; }
        
        public int HealthPoints
        {
            get => healthPoints; 
            set => healthPoints = value;
        }
        
        public BlockTypes BlockType
        {
            get => blockType; 
            set => blockType = value;
        }
        public BaseBlockView BlockPrefab 
        {
            get => blockPrefab; 
            set => blockPrefab = value;
        }
    }
}