using UnityEngine;

namespace Core.Statics
{
    public class TransformHelper
    {
        public static Vector2 ResizePosition(Vector2 position) =>
            AppConfig.Instance.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width * position.x, Screen.height * position.y, 0));

        public static Vector2 ResizeScale(Vector2 cellSize, SpriteRenderer spriteRenderer)
        {
            var sprite = spriteRenderer.sprite;
            var size = sprite.textureRect.size / sprite.pixelsPerUnit;
            var unitsHeight = 2 * AppConfig.Instance.MainCamera.orthographicSize;
            var unitsWidth = unitsHeight / Screen.height * Screen.width;
            var scaledSize = unitsWidth * cellSize.x / size.x;
            return new Vector2(scaledSize, scaledSize);
        }

        public static bool ObjectAtGamefield(Vector3 objectPosition)
        {
            var position = AppConfig.Instance.MainCamera.WorldToViewportPoint(objectPosition);

            if (position.x >= 0 && position.x <= 1 &&
                position.y >= 0 && position.y <= 1)
            {
                return true;
            }

            return false;
        }
    }
}