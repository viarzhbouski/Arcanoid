using UnityEngine;

namespace Scripts.Helpers
{
    public class ResizeHelper
    {
        public static Vector2 ResizePosition(Vector2 position, Camera camera) =>
            camera.ScreenToWorldPoint(new Vector3(Screen.width * position.x, Screen.height * position.y, 0));

        public static Vector2 ResizeScale(Vector2 cellSize, Camera camera, SpriteRenderer spriteRenderer)
        {
            var sprite = spriteRenderer.sprite;
            var size = sprite.textureRect.size / sprite.pixelsPerUnit;
            var unitsHeight = 2 * camera.orthographicSize;
            var unitsWidth = unitsHeight / Screen.height * Screen.width;
            var scaledSize = unitsWidth * cellSize.x / size.x;
            return new Vector2(scaledSize, scaledSize);
        }
    }
}