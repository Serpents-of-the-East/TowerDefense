using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class MinionEnemy
    {
        public static GameObject CreateMinionEnemy()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new Enemy() { speed = 5.0f });
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(2));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));
            gameObject.Add(new EnemyHealth() { health = 10f });
            gameObject.Add(new PointsComponent() { points = 5 });

            // Should have a health component as well... This must be created.

            return gameObject;

        }
    }
}
