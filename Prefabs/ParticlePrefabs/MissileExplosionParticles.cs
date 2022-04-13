using System;
using Microsoft.Xna.Framework;

using CrowEngineBase;

namespace TowerDefense
{
    public static class MissileExplosionParticles
    {
        public static GameObject Create(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(position, 0, Vector2.One));

            Particle particle = new Particle(ResourceManager.GetTexture("missile-explosion-particle"));

            particle.rate = TimeSpan.FromMilliseconds(2);
            particle.maxSpeed = 300;
            particle.minSpeed = 50;
            particle.minScale = 0.5f;
            particle.maxScale = 1;
            particle.rotationSpeed = 1;
            particle.renderDepth = 1;
            particle.maxLifeTime = TimeSpan.FromMilliseconds(600);
            particle.maxSystemLifetime = TimeSpan.FromMilliseconds(50);

            gameObject.Add(particle);

            return gameObject;
        }
    }
}
