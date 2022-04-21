using System;
using System.Collections.Generic;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class FlyingEnemy
    {
        public static GameObject Create(Vector2 position, SystemManager systemManager, PathGoal pathGoal)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Enemy());
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(25));
            gameObject.Add(new EnemyTag(EnemyType.AIR));
            gameObject.Add(new AnimatedSprite(ResourceManager.GetTexture("wyvern"), new int[] { 125, 125, 125, 125, 125, 125 }, Vector2.One * 64));
            gameObject.Add(new PointsComponent() { points = 50 });
            gameObject.Add(new Transform(position, 0, Vector2.One * 3));
            gameObject.Add(new BasicEnemyTestScript(gameObject, systemManager, 100));
            gameObject.Add(new Path() { goal = pathGoal });

            gameObject.Add(new EnemyHealth()
            {
                health = 100f,
                maxHealth = 100f,
                instantiateOnDeathObject = new List<GameObject>()
                {
                    EnemyDeathParticles.Create(Vector2.Zero),

                }
            });

            // Should have a health component as well... This must be created.

            return gameObject;
        }

    }
}
