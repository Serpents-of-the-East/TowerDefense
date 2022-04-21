﻿using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class RegularTower
    {
        public static float TowerRadius = Pathfinder.SIZE_PER_TOWER * 2;
        public static GameObject Create(SystemManager systemManager)
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new TowerComponent() { turnSpeed = 6 });
            gameObject.Add(new Sprite(ResourceManager.GetTexture("regularTower"), Color.White, 0));
            gameObject.Add(new CircleCollider(TowerRadius));
            gameObject.Add(new PointsComponent() { points = 100 });
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One * 2));
            gameObject.Add(new EnemyTag(EnemyType.GROUND));


            gameObject.Add(new TowerTargetingScript(gameObject, TowerTargetingScript.BulletType.Basic, TimeSpan.FromMilliseconds(500), systemManager));

            systemManager.Add(TowerColliderPrefab.Create(gameObject));


            return gameObject;
        }



    }
}
