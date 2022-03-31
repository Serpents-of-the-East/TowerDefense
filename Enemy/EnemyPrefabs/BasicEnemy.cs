using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class BasicEnemy
    {
        

        public static GameObject CreateBasicEnemy()
        {
            GameObject gameObject = new();
            gameObject.Add(new Enemy());
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(5));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));

            // Should have a health component as well... This must be created.

            return gameObject;
        }
    }
}
