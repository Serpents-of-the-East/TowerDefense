using System;
namespace CrowEngineBase
{
    public class CircleCollider : Collider
    {
        // The radius of the rectangle collider, centered at the object's position
        public float radius { get; set; }
        public CircleCollider(float radius)
        {
            this.radius = radius;
        }
    }
}
