using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.Graphics;
using System.Collections.Generic;
using System;

namespace LudumDare32
{
    class Level
    {
        public List<LevelItem> Items { get; set; }

        public Level()
        {
            Items = new List<LevelItem>();
        }

        public void Add(LevelItem item)
        {
            Items.Add(item);
        }

        private float CalculateMinProjection(RectangleF collider, Vector2 axis)
        {
            return
                Math.Min(Vector2.Dot(collider.TopLeft, axis),
                    Math.Min(Vector2.Dot(collider.TopRight, axis),
                        Math.Min(Vector2.Dot(collider.BottomLeft, axis),
                            Vector2.Dot(collider.BottomRight, axis))));
        }

        private float CalculateMaxProjection(RectangleF collider, Vector2 axis)
        {
            return
                Math.Max(Vector2.Dot(collider.TopLeft, axis),
                    Math.Max(Vector2.Dot(collider.TopRight, axis),
                        Math.Max(Vector2.Dot(collider.BottomLeft, axis),
                            Vector2.Dot(collider.BottomRight, axis))));
        }

        private bool CheckAxis(RectangleF a, RectangleF b, Vector2 axis, out float resolution)
        {
            resolution = 0;

            float aCenterProj = Vector2.Dot(a.Center, axis);
            float bCenterProj = Vector2.Dot(b.Center, axis);

            float aMin = CalculateMinProjection(a, axis);
            float aMax = CalculateMaxProjection(a, axis);
            float bMin = CalculateMinProjection(b, axis);
            float bMax = CalculateMaxProjection(b, axis);

            if (aCenterProj < bCenterProj)
            {
                if (aMax <= bMin)
                    return false;
                if (aMin > bMax)
                    return false;
                resolution = bMin - aMax;
                return true;
            }
            else
            {
                if (bMax <= aMin)
                    return false;
                if (bMin > aMax)
                    return false;
                resolution = aMin - bMax;
                return true;
            }

        }

        public CollisionResult CheckCollision(RectangleF collider, bool assertValidResolution = true)
        {
            bool collided = false;
            Vector2 axisResult = Vector2.Zero;
            float resolutionResult = float.PositiveInfinity;

            for (var i = 0; i < Items.Count; i++)
            {
                Vector2 thisAxisResult = Vector2.Zero;
                float thisResolutionResult = float.PositiveInfinity;

                float resolution;

                if (!CheckAxis(collider, Items[i].Collider, Vector2.UnitX, out resolution))
                    continue;

                if (Math.Abs(resolution) < Math.Abs(thisResolutionResult))
                {
                    thisResolutionResult = resolution;
                    thisAxisResult = Vector2.UnitX;
                }

                if (!CheckAxis(collider, Items[i].Collider, Vector2.UnitY, out resolution))
                    continue;

                if (Math.Abs(resolution) < Math.Abs(thisResolutionResult))
                {
                    thisResolutionResult = resolution;
                    thisAxisResult = Vector2.UnitY;
                }

                Vector2 resolutionVector = thisResolutionResult * thisAxisResult;

                if (assertValidResolution && CheckCollision(new RectangleF(collider.X + resolutionVector.X, collider.Y + resolutionVector.Y, collider.Width, collider.Height), false).Collided)
                    continue;

                if (Math.Abs(thisResolutionResult) < Math.Abs(resolutionResult))
                {
                    resolutionResult = thisResolutionResult;
                    axisResult = thisAxisResult;
                }

                collided = true;
            }

            return new CollisionResult(collided, axisResult * resolutionResult);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Items[i].Sprite.Draw(spriteBatch, Items[i].Position);
            }
        }
    }
}
