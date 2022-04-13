﻿using System;
using CrowEngineBase;
using Microsoft.Xna.Framework;

namespace TowerDefense
{
    public static class GuidedMissileTower
    {
        public static GameObject Create()
        {
            GameObject gameObject = new GameObject();

            gameObject.Add(new GuidedMissile());
            gameObject.Add(new TowerComponent() { turnSpeed = 3 });
            gameObject.Add(new Sprite(ResourceManager.GetTexture("guidedTower"), Color.White, 0));
            gameObject.Add(new CircleCollider(Pathfinder.SIZE_PER_TOWER*2));
            gameObject.Add(new PointsComponent() { points = 300 });
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            gameObject.Add(new EnemyTag(EnemyType.AIR));

            gameObject.Add(new TowerTargetingScript(gameObject));


            return gameObject;
        }
    }
}
