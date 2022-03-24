using System;
using Common.Enums;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [Serializable]
    public struct Block
    {
        [SerializeField]
        private int healthPoints;
        [SerializeField]
        private BlockTypes blockType;
        [SerializeField]
        private Color color;
        [SerializeField]
        private BaseBlockView blockPrefab;
        public BoostTypes? BoostType { get; set; }

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
        
        public Color Color 
        {
            get => color; 
            set => color = value;
        }
        
        public BaseBlockView BlockPrefab 
        {
            get => blockPrefab; 
            set => blockPrefab = value;
        }

        public Vector2 Position { get; set; }
    }
}