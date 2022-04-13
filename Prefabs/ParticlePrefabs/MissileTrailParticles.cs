using System;

using CrowEngineBase;

namespace TowerDefense
{
    /// <summary>
    /// Creates a particle component rather than the full component itself, since this should just attach to other objects
    /// </summary>
    public static class MissileTrailParticles
    {
        /// <summary>
        /// Create the particle emitter for the missile trail
        /// </summary>
        /// <returns></returns>
        public static Particle Create()
        {
            Particle particle = new Particle(ResourceManager.GetTexture("missile-explosion-particle"));

            particle.rate = TimeSpan.FromMilliseconds(100);
            particle.maxSpeed = 100;
            particle.minSpeed = 50;
            particle.minScale = 0.25f;
            particle.maxScale = 1;
            particle.rotationSpeed = 1;
            particle.renderDepth = 1;
            particle.maxLifeTime = TimeSpan.FromMilliseconds(600);
            particle.maxSystemLifetime = TimeSpan.FromSeconds(100000); // really long cause this should play the whole time the bomb is going

            return particle;
        }
    }
}
