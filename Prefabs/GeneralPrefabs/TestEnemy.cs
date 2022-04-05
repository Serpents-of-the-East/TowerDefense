﻿using System;

using CrowEngineBase;

using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class TestEnemy
    {
        public static GameObject Create(Vector2 startPosition)
        {
            GameObject gameObject = new GameObject();
            gameObject.Add(new Transform(startPosition, 0, Vector2.One));
            gameObject.Add(new CircleCollider(20));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Sprite(ResourceManager.GetTexture("bombTower"), Color.White));
            gameObject.Add(new Path() { goal = PathGoal.Right });
            gameObject.Add(new BasicEnemyTestScript(gameObject));

            return gameObject;
        }
    }
}
