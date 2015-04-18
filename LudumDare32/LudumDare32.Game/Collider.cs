using SiliconStudio.Core.Mathematics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LudumDare32
{
    interface Collider
    {
        Vector2[] GetAxis();
        Collider Clone();
        void Translate(Vector2 position);
        float MinProjection(Vector2 axis);
        float MaxProjection(Vector2 axis);

        RectangleF GetRect();
    }
}
