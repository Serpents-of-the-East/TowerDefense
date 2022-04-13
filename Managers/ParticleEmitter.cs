using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class ParticleEmitter
    {
        public static SystemManager systemManager;


        public static void EmitParticles(Vector2 position, string texture)
        {
            GameObject gameObject = new GameObject();
            Particle particle = new Particle(ResourceManager.GetTexture(texture));
            gameObject.Add(new Transform(position, 0, Vector2.One));

            systemManager.Add(gameObject);
        }



        public static void EmitSellParticles(Vector2 position)
        {
            if (systemManager == null)
            {
                throw new Exception("System Manager not set to a reference");
            }
            systemManager.Add(SellParticles.CreateSellParticles(position));
        }

        public static void EmitEnemyDeathParticles(Vector2 position)
        {
            if (systemManager == null)
            {
                throw new Exception("System Manager not set to a reference");
            }
            systemManager.Add(EnemyDeathParticles.Create(position));
        }

        public static void EmitBombExplosionParticles(Vector2 position)
        {
            if (systemManager == null)
            {
                throw new Exception("System Manager not set to a reference");
            }
            systemManager.Add(BombExplosionParticles.Create(position));
        }

        public static void EmitMissileExplosionParticles(Vector2 position)
        {
            if (systemManager == null)
            {
                throw new Exception("System Manager not set to a reference");
            }
            systemManager.Add(MissileExplosionParticles.Create(position));
        }

        //TODO: Keep adding more as we get more particles

    }
}