using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.Graphics;
using System.Collections.Generic;
using System;

namespace LudumDare32
{
    class LevelItem
    {
        public LevelItem(Sprite sprite, Vector2 position)
        {
            Sprite = sprite;
            Position = position;
            Collider = new RectangleF(position.X - sprite.Center.X, position.Y - sprite.Center.Y, sprite.Texture.Width, sprite.Texture.Height);
        }

        public Sprite Sprite { get; private set; }
        public Vector2 Position { get; private set; }
        public RectangleF Collider { get; private set; }
    }
}
