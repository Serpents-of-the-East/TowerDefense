using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class SummonerEnemy
    {
        public static GameObject CreateSummonerEnemy()
        {
            GameObject gameObject = new();

            gameObject.Add(new Enemy() { speed = 1.0f });
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(2));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));
            gameObject.Add(new EnemyHealth() { health = 75f });
            gameObject.Add(new PointsComponent() { points = 25 });

            // Should have a health component as well... This must be created.

            return gameObject;
        }


    }
}
