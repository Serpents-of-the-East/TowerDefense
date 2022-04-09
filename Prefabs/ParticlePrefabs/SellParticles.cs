using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class SellParticles
    {
        public static GameObject CreateSellParticles(Vector2 position)
        {
            GameObject gameObject = new GameObject();

            Particle particle = new Particle(ResourceManager.GetTexture("coin"));

            particle.rate = TimeSpan.FromMilliseconds(10);
            particle.maxSpeed = 1000;
            particle.minSpeed = 100;
            particle.minScale = 0.5f;
            particle.maxScale = 1;
            particle.rotationSpeed = 5f;
            particle.renderDepth = 1;
            particle.maxLifeTime = TimeSpan.FromMilliseconds(100);
            particle.emissionArea = Vector2.One * 0.5f;


            gameObject.Add(particle);
            gameObject.Add(new Transform(position, 0, Vector2.One));



            return gameObject;
        }
    }
}