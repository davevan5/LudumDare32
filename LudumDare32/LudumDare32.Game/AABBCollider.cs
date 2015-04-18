using SiliconStudio.Core.Mathematics;
using System.Collections.Generic;
using System;

namespace LudumDare32
{
    class AABBCollider : Collider
    {
        private static Vector2[] axis = new Vector2[] { Vector2.UnitX, Vector2.UnitY };
        private RectangleF rect;

        public AABBCollider(RectangleF rect)
        {
            this.rect = rect;
        }

        public Collider Clone()
        {
            return new AABBCollider(rect);
        }

        public void Translate(Vector2 position)
        {
            rect.X = rect.X + position.X;
            rect.Y = rect.Y + position.Y;
        }

        public Vector2[] GetAxis()
        {
            return axis;
        }

        public float MinProjection(Vector2 axis)
        {
            return
                Math.Min(Vector2.Dot(rect.TopLeft, axis),
                    Math.Min(Vector2.Dot(rect.TopRight, axis),
                        Math.Min(Vector2.Dot(rect.BottomLeft, axis),
                            Vector2.Dot(rect.BottomRight, axis))));

        }

        public float MaxProjection(Vector2 axis)
        {
            return
                Math.Max(Vector2.Dot(rect.TopLeft, axis),
                    Math.Max(Vector2.Dot(rect.TopRight, axis),
                        Math.Max(Vector2.Dot(rect.BottomLeft, axis),
                            Vector2.Dot(rect.BottomRight, axis))));
        }

        public RectangleF GetRect()
        {
            return rect;
        }
    }
}
