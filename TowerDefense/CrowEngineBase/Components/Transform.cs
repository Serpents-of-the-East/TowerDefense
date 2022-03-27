using System;

using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    public class Transform : Component
    {
        public Vector2 position { get; set; }
        public float rotation { get; set; }
        public Vector2 scale { get; set; }

        public Transform(Vector2 position, float rotation, Vector2 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }
}
