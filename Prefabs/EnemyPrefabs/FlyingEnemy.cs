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
            gameObject.Add(new PointsComponent() { points = 70 });
            gameObject.Add(new Transform(position, 0, Vector2.One * 3));
            gameObject.Add(new BasicEnemyTestScript(gameObject, systemManager, MathF.Min(100 + 5 * GameStats.numberLevels, 150)));
            gameObject.Add(new Path() { goal = pathGoal });

            gameObject.Add(new EnemyHealth()
            {
                health = MathF.Min(100 + 20 * GameStats.numberLevels, 500),
                maxHealth = MathF.Min(100 + 20 * GameStats.numberLevels, 500),
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
