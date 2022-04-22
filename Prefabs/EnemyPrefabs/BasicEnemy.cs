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
            gameObject.Add(new BasicEnemyTestScript(gameObject, systemManager, MathF.Min(100 + 10 * GameStats.numberLevels, 150)));
            gameObject.Add(new PointsComponent() { points = 30 });

            gameObject.Add(new Path() { goal = pathGoal });

            

            gameObject.Add(new EnemyHealth()
            {
                health = MathF.Min(100 + 10 * GameStats.numberLevels, 400),
                maxHealth = MathF.Min(100 + 10 * GameStats.numberLevels, 400),
                instantiateOnDeathObject = new List<GameObject>()
                {
                    EnemyDeathParticles.Create(gameObject.GetComponent<Transform>().position),
                    PointsTextPrefab.Create(gameObject.GetComponent<Transform>().position, systemManager, gameObject.GetComponent<PointsComponent>()),

                }
            });

            systemManager.DelayedAdd(EnemyHealthBar.CreateEnemyHealthBar(gameObject, systemManager));

            return gameObject;
        }
    }
}
