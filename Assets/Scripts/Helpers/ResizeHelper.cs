using UnityEngine;

namespace Scripts.Helpers
{
    public class ResizeHelper
    {
        public static Vector2 ResizePosition(float width, float height, Camera camera) =>
            camera.ScreenToWorldPoint(new Vector3(Screen.width * width, Screen.height * height, 0));

        public static Vector2 ResizeScale(float width, float height, Camera camera, SpriteRenderer spriteRenderer)
        {
            var sprite = spriteRenderer.sprite;
            var size = sprite.textureRect.size / sprite.pixelsPerUnit;
            var unitsHeight = 2 * camera.orthographicSize;
            var unitsWidth = unitsHeight / Screen.height * Screen.width;

            return new Vector2(unitsWidth * width / size.x, unitsHeight * height / size.y);
        }
    }
}