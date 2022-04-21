using System;
using System.Collections.Generic;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public class TankyEnemy
    {
        public static GameObject CreateTankyEnemy(Vector2 position, SystemManager systemManager, PathGoal pathGoal)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Enemy());
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(position, 0, Vector2.One * 4));
            gameObject.Add(new CircleCollider(20));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));

            gameObject.Add(new AnimatedSprite(ResourceManager.GetTexture("orc"), new int[] { 250, 250, 250, 250, 250, 250, 250, 250 }, Vector2.One * 64));

            gameObject.Add(new BasicEnemyTestScript(gameObject, systemManager));
            gameObject.Add(new PointsComponent() { points = 120 });
            gameObject.Add(new Path() { goal = pathGoal });


            gameObject.Add(new EnemyHealth()
            {
                health = 100f,
                maxHealth = 100f,
                instantiateOnDeathObject = new List<GameObject>()
                {


                }
            });

            systemManager.DelayedAdd(EnemyHealthBar.CreateEnemyHealthBar(gameObject, systemManager));

            return gameObject;
        }
    }
}
