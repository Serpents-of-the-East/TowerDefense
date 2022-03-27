using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngineBase
{
    /// <summary>
    /// This is NOT a single particle. Instead, it is a GROUP of particles, of all the same type.
    /// </summary>
    public class Particle : Component
    {
        /// <summary>
        /// The list of current particles
        /// </summary>
        public List<SingleParticle> particles { get; set; }

        /// <summary>
        /// The texture for this group of particles
        /// </summary>
        public Texture2D texture { get; set; }

        /// <summary>
        /// This is the possible bounds which a particle could spawn. It is relative to the CENTER of the particle
        /// component. If you want it all on one point emmision, this should be Vector2.Zero. For a square area, Vector2.One * size you want
        /// The center of the group is based on the transform of the gameObject is it attached to (you NEED a transform for this to render)
        /// </summary>
        public Vector2 emissionArea { get; set; }

        /// <summary>
        /// The lifetime of this particle group. Once this timer runs out, the group no longer spawns any new particles,
        /// and then will be removed from the system by the 
        /// </summary>
        public TimeSpan maxLifeTime { get; set; }

        /// <summary>
        /// TimeSpan between spawning a particle
        /// </summary>
        public TimeSpan rate { get; set; }

        /// <summary>
        /// The relative current time of the group, used for spawn rate and removal
        /// </summary>
        public TimeSpan currentTime { get; set; }

        public float minScale { get; set; }
        public float maxScale { get; set; }

        public float minSpeed { get; set; }
        public float maxSpeed { get; set; }

        public int renderDepth { get; set; }


        /// <summary>
        /// The speed which each particle should rotate
        /// </summary>
        public float rotationSpeed { get; set; }

        public Utilities.CrowRandom random { get; set; }

        public Particle(Texture2D particleTexture)
        {
            texture = particleTexture;
            particles = new List<SingleParticle>();
            currentTime = new TimeSpan();
            emissionArea = new Vector2();
            maxLifeTime = new TimeSpan();
            minScale = 1;
            maxScale = 2;
            minSpeed = 1;
            maxSpeed = 100;
            rate = new TimeSpan();
            renderDepth = 0;
            random = new Utilities.CrowRandom();
        }
    }

    /// <summary>
    /// This represents a single particle. The position is set using current transform location when created,
    /// and then from there, updated on it's own.
    /// </summary>
    public class SingleParticle
    {
        public Vector2 position { get; set; }
        public float rotation { get; set; }
        public float scale { get; set; }
        public Vector2 velocity { get; set; }
        public TimeSpan lifeTime { get; set; }

        public SingleParticle()
        {
            this.position = new Vector2();
            this.rotation = 0;
            this.scale = 1;
            this.velocity = new Vector2();
            this.lifeTime = new TimeSpan();
        }
    }
}
