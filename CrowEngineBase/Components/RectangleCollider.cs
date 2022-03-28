using System;

using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    public class RectangleCollider : Collider
    {
        // The size of the rectangle collider, centered at the object's position
        public Vector2 size { get; set; }

        public RectangleCollider(Vector2 size)
        {
            this.size = size;
        }
    }
}
