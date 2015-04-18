using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.Graphics;
using System.Collections.Generic;
using System;

namespace LudumDare32
{
    class LevelItem
    {
        public LevelItem(Sprite sprite, Vector2 position, float rotation = 0, Collider collider = null)
        {
            Sprite = sprite;
            Position = position;
            Rotation = rotation;

            Collider = collider ?? GetDefaultCollider(sprite, position, rotation);
        }

        private static Collider GetDefaultCollider(Sprite sprite, Vector2 position, float rotation)
        {
            if (rotation == 0 || rotation == 180)
                return new AABBCollider(
                    new RectangleF(
                        position.X - sprite.Center.X,
                        position.Y - sprite.Center.Y,
                        sprite.Texture.Width,
                        sprite.Texture.Height));

            if (rotation == 90 || rotation == 270)
                return new AABBCollider(
                    new RectangleF(
                        position.Y - sprite.Center.Y,
                        position.X - sprite.Center.X,
                        sprite.Texture.Height,
                        sprite.Texture.Width));

            return new BoxCollider(
                new RectangleF(
                    position.X - sprite.Center.X,
                    position.Y - sprite.Center.Y,
                    sprite.Texture.Width,
                    sprite.Texture.Height),
                rotation);
        }

        public Sprite Sprite { get; private set; }
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public Collider Collider { get; private set; }
    }
}
