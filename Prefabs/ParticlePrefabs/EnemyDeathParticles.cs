using System;
using Microsoft.Xna.Framework;

using CrowEngineBase;

namespace TowerDefense
{
    public static class EnemyDeathParticles
    {
        public static GameObject Create(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(position, 0, Vector2.One));

            Particle particle = new Particle(ResourceManager.GetTexture("enemy-death-particle"));

            particle.rate = TimeSpan.FromMilliseconds(10);
            particle.maxSpeed = 200;
            particle.minSpeed = 50;
            particle.minScale = 0.5f;
            particle.maxScale = 2;
            particle.rotationSpeed = 1;
            particle.renderDepth = 1;
            particle.maxLifeTime = TimeSpan.FromMilliseconds(600);
            particle.maxSystemLifetime = TimeSpan.FromMilliseconds(250);
            gameObject.Add(particle);

            return gameObject;
        }
    }
}
