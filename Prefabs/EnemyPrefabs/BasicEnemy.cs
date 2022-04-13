using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TowerDefense
{
    public static class BasicEnemy
    {
        public static GameObject CreateBasicEnemy(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Enemy());
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(position, 0, Vector2.One));
            gameObject.Add(new CircleCollider(20));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));
            gameObject.Add(new BasicEnemyTestScript(gameObject));
            gameObject.Add(new PointsComponent() { points = 20 });
            gameObject.Add(new Path() { goal = PathGoal.Right });
            Pathfinder.CheckPathsFunc.Invoke();


            gameObject.Add(new EnemyHealth()
            {
                health = 100f,
                maxHealth = 100f,
                instantiateOnDeathObject = new List<GameObject>()
                {


                }
            });






            return gameObject;
        }
    }
}
