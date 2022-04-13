﻿using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class FlyingEnemy
    {
        public static GameObject CreateFlyingEnemy(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Enemy());
            gameObject.Add(new Rigidbody());
            gameObject.Add(new CircleCollider(5));
            gameObject.Add(new EnemyTag(EnemyType.AIR));
            gameObject.Add(new Sprite(ResourceManager.GetTexture("crow"), Color.White, 0));
            gameObject.Add(new EnemyHealth() { health = 100f });
            gameObject.Add(new PointsComponent() { points = 20 });
            gameObject.Add(new Transform(position, 0, Vector2.One));

            // Should have a health component as well... This must be created.

            return gameObject;
        }

    }
}
