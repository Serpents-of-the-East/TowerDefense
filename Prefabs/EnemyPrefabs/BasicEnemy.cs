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
            gameObject.Add(new CircleCollider(5));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));
            gameObject.Add(new PointsComponent() { points = 20 });
            //TODO: This is where we will declare our other prefabs like particle effects and points


            


            
            return gameObject;
        }
    }
}
