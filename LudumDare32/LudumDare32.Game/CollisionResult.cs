using SiliconStudio.Core.Mathematics;
using System.Collections.Generic;
using System;

namespace LudumDare32
{
    class CollisionResult
    {
        public CollisionResult(bool collided, Vector2 resolution)
        {
            Collided = collided;
            Resolution = resolution;
        }

        public bool Collided { get; private set; }
        public Vector2 Resolution { get; private set; }
    }
}
