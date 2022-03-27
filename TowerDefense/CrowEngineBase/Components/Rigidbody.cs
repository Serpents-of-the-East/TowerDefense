using System;

using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace CrowEngineBase
{
    public class Rigidbody : Component
    {
        public float mass { get; set; }
        public float gravityScale { get; set; }
        public Vector2 velocity { get; set; }
        public Vector2 acceleration { get; set; }

        /// <summary>
        /// The list of id's of gameobjects on which this rigidbody is colliding with (previous frame)
        /// </summary>
        public List<uint> currentCollidedGameObjects { get; set; }

        public Rigidbody(float mass=0, float gravityScale=0) : this(new Vector2(0, 0), new Vector2(0, 0), mass, gravityScale)
        {
        }




        public Rigidbody(Vector2 startVelocity, float mass=0, float gravityScale = 0) : this(new Vector2(0, 0), startVelocity, mass, gravityScale)
        {
        }

        public Rigidbody(Vector2 acceleration, Vector2 startVelocity, float mass=0, float gravityScale = 0)
        {
            this.mass = mass;
            this.gravityScale = gravityScale;
            this.velocity = startVelocity;
            this.currentCollidedGameObjects = new List<uint>();
            this.acceleration = acceleration;
        }


    }
}
