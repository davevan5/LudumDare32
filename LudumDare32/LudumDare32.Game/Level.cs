using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
using SiliconStudio.Paradox.EntityModel;
using System.Diagnostics;

namespace LudumDare32
{
    class Tile
    {
        public Sprite Sprite { get; set; }
        public TileExposedSide Exposed { get; set; }
        public bool Collidable { get; set; }
    }

    [Flags]
    enum TileExposedSide
    {
        None = 0,
        Top = 1 << 0,
        Left = 1 << 1,
        Right = 1 << 2,
        Bottom = 1 << 3
    }

    struct Position
    {
        public Position(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
    }


    class Level
    {
        public readonly Tile[] tiles;
        private readonly Size2 size;

        public Level(Size2 size)
        {
            this.size = size;
            tiles = new Tile[size.Width * size.Height];

        }

        private int GetTileIndex(int x, int y)
        {
            return (y * size.Width) + x;
        }

        public void SetTile(int x, int y, Tile tile)
        {
            tiles[GetTileIndex(x, y)] = tile;
        }

        public Position TileFromPixelCoordinate(Vector2 pixel)
        {
            return new Position(
                MathUtil.Clamp((int)(pixel.X / 32), 0, size.Width - 1),
                MathUtil.Clamp((int)(pixel.Y / 32), 0, size.Height - 1));
        }

        public void BuildTileData()
        {
            for (int y = 0; y < size.Height; y++)
                for (int x = 0; x < size.Width; x++)
                {
                    var tile = tiles[GetTileIndex(x, y)];
                    if (tile == null)
                        continue;

                    TileExposedSide exposed = TileExposedSide.None;

                    if (x == 0 || tiles[GetTileIndex(x - 1, y)] == null)
                    {
                        exposed |= TileExposedSide.Left;
                    }

                    if (x == size.Width - 1 || tiles[GetTileIndex(x + 1, y)] == null)
                    {
                        exposed |= TileExposedSide.Right;
                    }

                    if (y == 0 || tiles[GetTileIndex(x, y - 1)] == null)
                    {
                        exposed |= TileExposedSide.Top;
                    }

                    if (y == size.Height - 1 || tiles[GetTileIndex(x, y + 1)] == null)
                    {
                        exposed |= TileExposedSide.Bottom;
                    }

                    tile.Exposed = exposed;
                }
        }

        private bool CheckAxis(Collider a, Collider b, Vector2 axis, out float resolution)
        {
            resolution = 0;

            float aMin = a.MinProjection(axis);
            float aMax = a.MaxProjection(axis);
            float bMin = b.MinProjection(axis);
            float bMax = b.MaxProjection(axis);
            float aHalf = (aMax - aMin) * 0.5f;
            float bHalf = (bMax - bMin) * 0.5f;
            float aCenterProj = aMin + aHalf;
            float bCenterProj = bMin + bHalf;

            float distance = Math.Abs(bCenterProj - aCenterProj);

            if (distance - aHalf - bHalf >= -float.Epsilon)
            {
                return false;
            }

            if (aCenterProj <= bCenterProj)
            {
                resolution = aMax - bMin;
            }
            else
            {
                resolution = bMax - aMin;
            }

            return true;
        }

        public void CheckCollision(Player player)
        {
            var rect = player.GetCollider();

            Position topLeft = TileFromPixelCoordinate(rect.TopLeft);
            Position bottomRight = TileFromPixelCoordinate(rect.BottomRight);



            for (int y = topLeft.Y; y <= bottomRight.Y; y++)
                for (int x = topLeft.X; x <= bottomRight.X; x++)
                {
                    bool collided = false;
                    float resolution = float.PositiveInfinity;
                    Vector2 axis = Vector2.Zero;

                    var tile = tiles[GetTileIndex(x,y)];
                    if (tile == null)
                        continue;

                    var tileRect = new RectangleF(x * 32, y * 32, 32, 32);

                    if (!rect.Intersects(tileRect))
                        continue;

                    // So we handle an individual tile
                    if (tileRect.Center.Y <= rect.Center.Y && (tile.Exposed & TileExposedSide.Bottom) == TileExposedSide.Bottom)
                    {
                        // We are above the player, and we have a solid bottom
                        float res = tileRect.Bottom - rect.Top;
                        if (res > 0 && res < resolution)
                        {
                            collided = true;
                            resolution = res;
                            axis = Vector2.UnitY;
                        }
                    }

                    if (tileRect.Center.Y > rect.Center.Y && (tile.Exposed & TileExposedSide.Top) == TileExposedSide.Top)
                    {
                        // We are above the player, and we have a solid bottom
                        float res = rect.Bottom - tileRect.Top;
                        if (res > 0 && res < resolution)
                        {
                            collided = true;
                            resolution = res;
                            axis = -Vector2.UnitY;
                        }
                    }

                    if (tileRect.Center.X <= rect.Center.X && (tile.Exposed & TileExposedSide.Right) == TileExposedSide.Right)
                    {
                        // We are above the player, and we have a solid bottom
                        float res = tileRect.Right - rect.Left;
                        if (res > 0 && res < resolution)
                        {
                            collided = true;
                            resolution = res;
                            axis = Vector2.UnitX;
                        }
                    }

                    if (tileRect.Center.X > rect.Center.X && (tile.Exposed & TileExposedSide.Left) == TileExposedSide.Left)
                    {
                        // We are above the player, and we have a solid bottom
                        float res = rect.Right - tileRect.Left;
                        if (res > 0 && res < resolution)
                        {
                            collided = true;
                            resolution = res;
                            axis = -Vector2.UnitX;
                        }
                    }

                    if (collided && float.IsInfinity(resolution))
                        Debugger.Break();

                    if (collided)
                        player.OnCollision(new CollisionResult(collided, axis * resolution));
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var y = 0; y < size.Height; y++)
                for (var x = 0; x < size.Width; x++)
                {
                    var tile = tiles[GetTileIndex(x, y)];
                    if (tile == null)
                        continue;
                    tile.Sprite.Draw(spriteBatch, new Vector2(x * 32 + 16, y * 32 + 16));
                }
        }
    }
}
