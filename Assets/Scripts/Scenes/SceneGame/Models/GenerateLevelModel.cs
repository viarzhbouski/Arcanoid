using System;
using Common.Enums;
using Core.Interfaces.MVC;
using UnityEngine;

namespace Scenes.SceneGame.Models
{
    public struct BlockInfo
    {
        public int HealthPoints { get; set; }
        
        public Color Color { get; set; }
        
        public Sprite Sprite { get; set; }
        
        public Vector2 Position { get; set; }
        
        public BlockTypes BlockType { get; set; }
        
        public BoostTypes? BoostType { get; set; }
    }
    
    public class GenerateLevelModel : IModel
    {
        public string LevelNumber { get; set; }
        
        public Sprite PackIcon { get; set; }
        
        public Vector2 CellSize { get; set; }
        
        public Vector2 TopPanelPosition { get; set; }
        
        public Vector2 StartPosition { get; set; }

        public BlockInfo[,] Blocks { get; set; }
        
        public Action DestroyBlockEvent { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}