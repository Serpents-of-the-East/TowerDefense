using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class TankyEnemy
    {
        public static GameObject CreateTankyEnemy()
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Enemy() { speed = 0.5f } );
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(5));
            gameObject.Add(new EnemyTag(EnemyType.AIR));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));
            gameObject.Add(new EnemyHealth() { health = 200f });
            gameObject.Add(new PointsComponent() { points = 50 });

            // Should have a health component as well... This must be created.

            return gameObject;
        }
    }
}
