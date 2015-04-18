using SiliconStudio.Core.Mathematics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LudumDare32
{
    static class Vector2Helpers
    {
        public static Vector2 RotateAround(this Vector2 vec, float r, Vector2 origin)
        {
            vec = vec - origin;

            return new Vector2(
                (float)(vec.X * Math.Cos(r) - vec.Y * Math.Sin(r)),
                (float)(vec.Y * Math.Cos(r) + vec.X * Math.Sin(r))) + origin;
        }

        public static Vector2 Rotate(this Vector2 vec, float r)
        {
            return vec.RotateAround(r, Vector2.Zero);
        }
    }
}
