using System;

using CrowEngineBase;

namespace TowerDefense
{
    /// <summary>
    /// Creates a particle component rather than the full component itself, since this should just attach to other objects
    /// </summary>
    public static class LightningTrailParticles
    {
        static LightningTrailParticles()
        {
            ResourceManager.RegisterTexture("Textures/lightningparticle", "lightningparticle");
        }

        /// <summary>
        /// Create the particle emitter for the bomb trail
        /// </summary>
        /// <returns></returns>
        public static Particle Create()
        {
            Particle particle = new Particle(ResourceManager.GetTexture("lightningparticle"));

            particle.rate = TimeSpan.FromMilliseconds(20);
            particle.maxSpeed = 200;
            particle.minSpeed = 100;
            particle.minScale = 0.5f;
            particle.maxScale = 1;
            particle.rotationSpeed = 1;
            particle.renderDepth = 1;
            particle.maxLifeTime = TimeSpan.FromMilliseconds(600);
            particle.maxSystemLifetime = TimeSpan.FromSeconds(100000); // really long cause this should play the whole time the bomb is going

            return particle;
        }
    }
}
