using SiliconStudio.Core.Mathematics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LudumDare32
{
    class BoxCollider : Collider
    {
        private Vector2[] axes;
        private RectangleF rect;
        private float rotation;

        public BoxCollider(RectangleF rect, float rotation)
        {
            this.rect = rect;
            this.rotation = rotation;

            axes = new Vector2[] {
                    Vector2.Normalize(Vector2.UnitX.Rotate(rotation)),
                    Vector2.Normalize(Vector2.UnitY.Rotate(rotation))
                };
        }

        public Vector2[] GetAxis()
        {
            return axes;
        }

        public Collider Clone()
        {
            return new BoxCollider(rect, rotation);
        }

        public void Translate(Vector2 position)
        {
            rect.X = rect.X + position.X;
            rect.Y = rect.Y + position.Y;
        }

        public float MinProjection(Vector2 axis)
        {
            return
                Math.Min(Vector2.Dot(rect.TopLeft.RotateAround(rotation, rect.Center), axis),
                    Math.Min(Vector2.Dot(rect.TopRight.RotateAround(rotation, rect.Center), axis),
                        Math.Min(Vector2.Dot(rect.BottomLeft.RotateAround(rotation, rect.Center), axis),
                            Vector2.Dot(rect.BottomRight.RotateAround(rotation, rect.Center), axis))));
        }

        public float MaxProjection(Vector2 axis)
        {
            return
                Math.Max(Vector2.Dot(rect.TopLeft.RotateAround(rotation, rect.Center), axis),
                    Math.Max(Vector2.Dot(rect.TopRight.RotateAround(rotation, rect.Center), axis),
                        Math.Max(Vector2.Dot(rect.BottomLeft.RotateAround(rotation, rect.Center), axis),
                            Vector2.Dot(rect.BottomRight.RotateAround(rotation, rect.Center), axis))));
        }

        public RectangleF GetRect()
        {
            return rect;
        }
    }
}
