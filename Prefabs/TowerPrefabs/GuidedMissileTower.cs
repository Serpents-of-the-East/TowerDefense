using System;
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
            gameObject.Add(new TowerComponent());
            gameObject.Add(new Sprite(ResourceManager.GetTexture("guidedTower"), Color.White, 0));
            gameObject.Add(new CircleCollider(10));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new Transform(Vector2.Zero, 0, Vector2.One));

            return gameObject;
        }
    }
}
