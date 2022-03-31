using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class FastEnemy
    {
        public static GameObject CreateFastEnemy()
        {
            GameObject gameObject = new();

            gameObject.Add(new Enemy() { speed = 5.0f }) ;
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(5));
            gameObject.Add(new EnemyTag(EnemyType.AIR));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));
            gameObject.Add(new EnemyHealth() { health = 50.0f });
            gameObject.Add(new PointsComponent() { points = 10 });

            // Should have a health component as well... This must be created.

            return gameObject;
        }
    }
}
