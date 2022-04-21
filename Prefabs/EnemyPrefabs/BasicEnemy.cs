using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TowerDefense
{
    public static class BasicEnemy
    {
        public static GameObject CreateBasicEnemy(Vector2 position, SystemManager systemManager, PathGoal pathGoal)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Enemy()); // SPEED WAS CHANGED
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(position, 0, Vector2.One * 3));
            gameObject.Add(new CircleCollider(20));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));

            gameObject.Add(new AnimatedSprite(ResourceManager.GetTexture("goblin"), new int[] { 250, 250, 250, 250, 250, 250, 250, 250 }, Vector2.One * 64));

            gameObject.Add(new BasicEnemyTestScript(gameObject, systemManager, 100));
            gameObject.Add(new PointsComponent() { points = 30 });
            gameObject.Add(new Path() { goal = pathGoal });


            gameObject.Add(new EnemyHealth()
            {
                health = 100f,
                maxHealth = 100f,
                instantiateOnDeathObject = new List<GameObject>()
                {
                    EnemyDeathParticles.Create(Vector2.Zero),

                }
            }) ;


            return gameObject;
        }
    }
}
