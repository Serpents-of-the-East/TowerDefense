using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TowerDefense
{
    public static class BasicEnemy
    {
        

        public static GameObject CreateBasicEnemy()
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Enemy());
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(5));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));
            gameObject.Add(new PointsComponent() { points = 20 });
            //TODO: This is where we will declare our other prefabs like particle effects and points


            gameObject.Add(new EnemyHealth() { health = 100f, instantiateOnDeathObject = new List<GameObject>()
            {


            } });

            // Should have a health component as well... This must be created.

            return gameObject;
        }
    }
}
