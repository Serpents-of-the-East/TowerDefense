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
            gameObject.Add(new Particle(ResourceManager.GetTexture("coin")));
            gameObject.Add(new Transform(position, 0, Vector2.One));



            return gameObject;
        }
    }
}
