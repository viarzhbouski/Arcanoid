using Common.Enums;
using UnityEngine;

namespace Scripts.Core.Models
{
    public class BlockPosition
    {
        public BlockTypes BlockType { get; set; }
        
        public Vector2 Position { get; set; }
    }
}